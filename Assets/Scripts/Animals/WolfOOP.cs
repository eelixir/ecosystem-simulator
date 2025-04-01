using UnityEngine;
using UnityEngine.AI;

public class WolfOOP : OrganismOOP
{
    // Camera and timer variables
    public GameObject WolfCamera;
    private float decreaseTimer = 0f;
    private float decreaseInterval = 1f;
    private float logTimer = 0f;
    private float logInterval = 1f; 

    // Organism Data UI variables
    public static bool CanvasOrganismDataUI = false;
    public static bool FreeCamControllerUpdater = false;
    public static string selectedWolfName;
    public static int selectedWolfHealth;
    public static int selectedWolfHunger;
    public static int selectedWolfThirst;
    public static int selectedWolfStamina;
    public static string selectedWolfMovementState;
    public static string selectedWolfBehavioralState;

    // Pathfinding variables
    public LayerMask detectionLayer;
    public float distanceToDeer;
    public float distanceToMate;
    public float distanceToWater;
    public int matingCooldown = 0;


    // Start function when script is first ran
    void Start()
    {
        // Declare variables for the organism
        agent = GetComponent<NavMeshAgent>();
        WolfCamera = GameObject.Find("Camera");
        isAlive = true;
        organismName = "Wolf";
        health = 100;
        healthMax = 100;
        hunger = 100;
        hungerMax = 100;
        thirst = 100;
        thirstMax = 100;
        stamina = 100;
        staminaMax = 100;
        movementState = "running";
        behaviouralState = "searchDeer";
        position = transform.position;
        radius = 10.0f;

        // Set the GameObject's name to the organisms name
        gameObject.name = organismName + "_" + gameObject.GetInstanceID();
        organismName = gameObject.name;

        Debug.Log(organismName + " initialized.");
    }


    // Update function
    void Update()
    {
        // Checks if organism is alive
        if (isAlive)
        {
            Detection();
            BehaviourUpdating();
            Pathfinding();

            // If organism is not alive then decrease population by 1 and set isAlive to false
            if (health <= 0)
            {
                organismDead();
            }
            // Makes sure hunger, thirst and stamina cannot go below 0
            else if (hunger <= 0) hunger = 0;
            else if (thirst <= 0) thirst = 0;
            else if (stamina <= 0) stamina = 0;

            // Updates movement state depending on stamina
            if (stamina <= 25)
            {
                movementState = "walking";
            }
            else if (stamina > 25)
            {
                movementState = "running";
            }

            // Decrease time by the frames that have passed
            decreaseTimer += Time.deltaTime;
            logTimer += Time.deltaTime;

            // Updates variables every second
            if (decreaseTimer >= decreaseInterval)
            {
                // Naturally  decrease hunger and thrist over time
                if (health >= 0)
                {
                    hunger -= 3;
                    thirst -= 3;
                }
                // If hunger or thirst is <= 50 then decrease health and stamina
                if (hunger <= 50 || thirst <= 50)
                {
                    health -= 3;
                    stamina -= 3;
                }
                // Decrease mating cooldown by 1 every second if it is greater than zero
                if (matingCooldown > 0)
                {
                    matingCooldown -= 1;
                }

                decreaseTimer = 0f;
            }

            // Log stats at the specified interval
            if (logTimer >= logInterval)
            {
                Debug.Log($"Health: {health}, Stamina: {stamina}, Hunger: {hunger}, Thirst: {thirst}");
                logTimer = 0f;
            }
        }
    }


    // Detection logic used to find distances to other objects
    public void Detection()
    {

        GameObject closestDeer = null;
        float closestDistanceToDeer = Mathf.Infinity;
        GameObject closestWater = null;
        float closestDistanceToWater = Mathf.Infinity;
        GameObject closestWolf = null;
        float closestDistanceToMate = Mathf.Infinity;


        // Detect the closest deer
        foreach (GameObject deer in GameObject.FindGameObjectsWithTag("Deer"))
        {
            float distance = Vector3.Distance(transform.position, deer.transform.position);
            if (distance < closestDistanceToDeer && distance <= 100f)
            {
                closestDistanceToDeer = distance;
                distanceToDeer = closestDistanceToDeer;
                closestDeer = deer;
            }
        }

        // Detect the closest water
        foreach (GameObject water in GameObject.FindGameObjectsWithTag("Water"))
        {
            float distance = Vector3.Distance(transform.position, water.transform.position);
            if (distance < closestDistanceToWater && distance <= 100f)
            {
                closestDistanceToWater = distance;
                distanceToWater = closestDistanceToWater;
                closestWater = water;
            }
        }

        // Detect the closest wolf for mating
        foreach (GameObject wolf in GameObject.FindGameObjectsWithTag("Wolf"))
        {
            if (wolf == gameObject || !wolf.activeInHierarchy)
            {
                continue; // Skip self or inactive wolf
            }

            WolfOOP potentialMate = wolf.GetComponent<WolfOOP>();
            if (potentialMate != null && potentialMate.behaviouralState != "mating")
            {
                // If potential mate is found and it's not already mating
                float distance = Vector3.Distance(transform.position, wolf.transform.position);
                if (distance < closestDistanceToMate && distance <= 100f)
                {
                    closestDistanceToMate = distance;
                    distanceToMate = closestDistanceToMate;
                    closestWolf = wolf;
                }
            }
        }
    }


    // Updates the Behavioural State depending on certain conditions
    void BehaviourUpdating()
    {
        // Define useful variables
        bool isHungry = hunger <= (hungerMax / 2);
        bool isThirsty = thirst <= (thirstMax / 2);
        bool needsFood = isHungry && hunger < thirst && distanceToDeer > 3f;
        bool needsWater = isThirsty && thirst < hunger && distanceToWater > 3f;
        bool nearDeer = distanceToDeer <= 3f;
        bool nearWater = distanceToWater <= 3f;
        bool nearMate = distanceToMate <= 3f;

        // Behavioural conditions with the priority order: searching for mate, then water, then food
        if (!isHungry && !isThirsty && (stamina > (staminaMax / 2)) && (matingCooldown == 0))
        {
            if (!nearMate)
            {
                behaviouralState = "searchMate";
            }
            else
            {
                behaviouralState = "mating";
            }
        }
        else if (needsFood)
        {
            behaviouralState = "searchDeer";
        }
        else if (needsWater)
        {
            behaviouralState = "searchWater";
        }
        else if (isHungry && isThirsty && hunger == thirst && distanceToWater > 1f)
        {
            behaviouralState = "searchWater";
        }
        else if (nearWater && nearDeer)
        {
            if (thirst <= hunger)
            {
                behaviouralState = "drinking";
            }
            else
            {
                behaviouralState = "eating";
            }
        }
        else if (nearDeer)
        {
            behaviouralState = "eating";
        }
        else if (nearWater)
        {
            behaviouralState = "drinking";
        }
        else
        {
            if (thirst <= hunger)
            {
                behaviouralState = "searchWater";
            }
            else
            {
                behaviouralState = "searchDeer";
            }
        }
    }


    // Function for the behavioural status of the wolf that determines the pathfinding
    void Pathfinding()
    {
        switch (behaviouralState)
        {
            case "searchDeer":
                SearchForDeer();
                break;

            case "searchWater":
                SearchForWater();
                break;

            case "searchMate":
                SearchForMate();
                break;

            case "eating":
                // Eating behavior
                hunger = hungerMax;
                break;

            case "drinking":
                // Drinking behavior
                thirst = thirstMax;
                break;

            case "mating":
                // Mating behavior
                matingCooldown = 20;
                break;

            default:
                Debug.LogError("Error: " + organismName + " has an unrecognized behaviouralState.");
                break;
        }
    }


    // Pathfinding to move wolf towards the closest deer
    void SearchForDeer()
    {
        GameObject closestDeer = null;
        float closestDistanceToDeer = Mathf.Infinity;

        // Find the closest deer
        foreach (GameObject deer in GameObject.FindGameObjectsWithTag("Deer"))
        {
            float distance = Vector3.Distance(transform.position, deer.transform.position);
            if (distance < closestDistanceToDeer && distance <= 100f)
            {
                closestDistanceToDeer = distance;
                closestDeer = deer;
            }
        }

        if (closestDeer != null)
        {
            if (closestDistanceToDeer <= agent.stoppingDistance * 1.5f)
            {
                agent.isStopped = true;
                return;
            }

            agent.isStopped = false;
            if (movementState == "running")
            {
                agent.speed = 8f;
            }
            else
            {
                agent.speed = 4f;
            }
            agent.SetDestination(closestDeer.transform.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }


    // Pathfinding to move wolf towards the closest water
    void SearchForWater()
    {
        GameObject closestWater = null;
        float closestDistanceToWater = Mathf.Infinity;

        // Find the closest water source
        foreach (GameObject water in GameObject.FindGameObjectsWithTag("Water"))
        {
            float distance = Vector3.Distance(transform.position, water.transform.position);
            if (distance < closestDistanceToWater && distance <= 100f)
            {
                closestDistanceToWater = distance;
                closestWater = water;
            }
        }

        if (closestWater != null)
        {
            if (closestDistanceToWater <= agent.stoppingDistance * 1.5f)
            {
                agent.isStopped = true;
                return;
            }

            agent.isStopped = false;
            if (movementState == "running")
            {
                agent.speed = 8f;
            }
            else
            {
                agent.speed = 4f;
            }
            agent.SetDestination(closestWater.transform.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }


    // Pathfinding to move wolf towards the closest mate
    void SearchForMate()
    {
        GameObject closestWolf = null;
        float closestDistanceToMate = Mathf.Infinity;

        // Find the closest potential mate
        foreach (GameObject wolf in GameObject.FindGameObjectsWithTag("Wolf"))
        {
            if (wolf == gameObject || !wolf.activeInHierarchy)
            {
                continue;
            }

            WolfOOP potentialMate = wolf.GetComponent<WolfOOP>();
            if (potentialMate != null && Vector3.Distance(transform.position, wolf.transform.position) < closestDistanceToMate)
            {
                closestDistanceToMate = Vector3.Distance(transform.position, wolf.transform.position);
                closestWolf = wolf;
            }
        }

        if (closestWolf != null)
        {
            agent.isStopped = false;
            if (movementState == "running")
            {
                agent.speed = 8f;
            }
            else
            {
                agent.speed = 4f;
            }
            agent.SetDestination(closestWolf.transform.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }


    // Method to detect when FreeCamPlayer left clicks on an orgnism when in range
    public void OnMouseOver()
    {
        GameObject freecamobject = GameObject.Find("FreeCamPlayer");
        float distance = Vector3.Distance(gameObject.transform.position, freecamobject.transform.position);

        if (distance <= 10 && Input.GetMouseButtonDown(0))
        {
            // Set static variables with current wolf's info
            selectedWolfName = organismName;
            selectedWolfHealth = health;
            selectedWolfHunger = hunger;
            selectedWolfThirst = thirst;
            selectedWolfStamina = stamina;
            selectedWolfMovementState = movementState;
            selectedWolfBehavioralState = behaviouralState;

            // Set the CanvasOrganismDataUI to true
            CanvasOrganismDataUI = true;
            FreeCamControllerUpdater = true;
        }
    }

    void organismDead()
    {
        health = 0;
        EnvironmentData.WolfPopulation -= 1;
        isAlive = false;
        // If organism is dead then destory gameObject
        Object.Destroy(gameObject);
    }
}
    