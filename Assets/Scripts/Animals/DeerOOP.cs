using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeerOOP : OrganismOOP
{
    public GameObject DeerCamera;
    private float decreaseTimer = 0f;
    private float decreaseInterval = 1f;
    private float logTimer = 0f;
    private float logInterval = 1f; // Set log frequency to 5 seconds

    // Organism Data UI
    public static bool CanvasOrganismDataUI = false;
    public static bool FreeCamControllerUpdater = false;
    public static string selectedDeerName;
    public static int selectedDeerHealth;
    public static int selectedDeerHunger;
    public static int selectedDeerThirst;
    public static int selectedDeerStamina;
    public static string selectedDeerMovementState;
    public static string selectedDeerBehavioralState;

    // Pathfinding
    public LayerMask detectionLayer;
    public float distanceToWolf;
    public float distanceToPlant;
    public float distanceToMate;


    void Start()
    {
        // Declare variables for the organism
        agent = GetComponent<NavMeshAgent>();
        DeerCamera = GameObject.Find("Camera");
        isAlive = true;
        organismName = "Deer";
        health = 100;
        healthMax = 100;
        hunger = 100;
        hungerMax = 100;
        thirst = 100;
        thirstMax = 100;
        stamina = 100;
        staminaMax = 100;
        movementState = "walking";
        behaviouralState = "idle";
        position = transform.position;
        radius = 5.0f;

        // Set the GameObject's name to the organisms name
        gameObject.name = organismName + "_" + gameObject.GetInstanceID();
        organismName = gameObject.name;

        Debug.Log(organismName + " initialized.");
    }

    void Update()
    {
        // Checks if organism is alive
        if (isAlive)
        {
            BehaviourUpdating();
            Pathfinding();

            // If organism is not alive then decrease population by 1 and set isAlive to false
            if (health <= 0)
            {
                health = 0;
                EnvironmentData.DeerPopulation -= 1;
                isAlive = false;
                // Destroy(gameObject);
            }
            else if (hunger <= 0)
            {
                hunger = 0;
            }
            else if (thirst <= 0)
            {
                thirst = 0;
            }
            else if (stamina <= 0)
            {
                stamina = 0;
            }


            // Decrease time by the frames that have passed
            decreaseTimer += Time.deltaTime;
            logTimer += Time.deltaTime;

            if (decreaseTimer >= decreaseInterval)
            {
                if (health >= 0)
                {
                    hunger -= 1;
                    thirst -= 1;
                }

                if (hunger <= 10 || thirst <= 10)
                {
                    health -= 3;
                    stamina -= 3;
                }

                decreaseTimer = 0f;
            }

            // Log stats at the specified interval
            if (logTimer >= logInterval)
            {
                Debug.Log($"Health: {health}, Stamina: {stamina}, Hunger: {hunger}, Thirst: {thirst}");
                logTimer = 0f;
            }

            if (stamina <= 0)
            {
                movementState = "walking";
            }
            else if (stamina >= 25)
            {
                movementState = "running";
            }
        }
    }


    // Updated the Behavioural State depending on certain conditions
    void BehaviourUpdating()
    {
        if (distanceToWolf < radius)
        {
            // Prioritises running from wolf
            behaviouralState = "runFromWolf";
            return;
        }
        else if ((hunger <= (hungerMax / 2)) && (hunger < thirst) && (distanceToPlant > 1f))
        {
            behaviouralState = "searchPlant";
        }
        else if ((thirst <= (thirstMax / 2)) && (thirst < hunger) && (distanceToWater > 1f))
        {
            behaviouralState = "searchWater";
        }
        else if ((thirst <= (thirstMax / 2)) && (hunger <= (hungerMax / 2) && (thirst == hunger)) && (distanceToWater > 1f))
        {
            behaviouralState = "searchWater";
        }
        else if ((distanceToWater <= 1f) && (distanceToPlant <= 1f))
        {
            if (thirst <= hunger)
            {
                behaviouralState = "drinking";
                return;
            }
            else
            {
                behaviouralState = "eating";
            }
        }
        else if (distanceToPlant <= 1f)
        {
            behaviouralState = "eating";
        }
        else if (distanceToWater <= 1f)
        {
            behaviouralState = "drinking";
            return;
        }
        else if ((hunger > (hungerMax / 2)) && (thirst > (thirstMax / 2)) && (stamina > (staminaMax / 2)) && (distanceToMate > 1f))
        {
            behaviouralState = "searchMate";
        }
        else if ((hunger > (hungerMax / 2)) && (thirst > (thirstMax / 2)) && (distanceToMate <= 1f))
        {
            behaviouralState = "mating";
        }
        else
        {
            behaviouralState = "idle";
            Debug.Log(organismName + " is idle. Possible problem!");
        }
    }

    void Pathfinding()
    {
        switch (behaviouralState)
        {
            case "runFromWolf":
                // Pathfinding to escape from wolf
                RunFromWolf();
                break;

            case "searchPlant":
                // Pathfinding to find plant
                SearchForPlant();
                break;

            case "searchWater":
                // Pathfinding to find water
                SearchForWater();
                break;

            case "searchMate":
                // Pathfinding to find a mate
                SearchForMate();
                break;

            case "eating":
                // Eating behavior
                decreaseTimer += Time.deltaTime;
                logTimer += Time.deltaTime;

                if (decreaseTimer >= decreaseInterval)
                {
                    hunger += 10;
                    if (hunger > hungerMax)
                    {
                        hunger = hungerMax;
                    }
                    decreaseTimer = 0f;
                }
                break;

            case "drinking":
                // Drinking behavior
                decreaseTimer += Time.deltaTime;
                logTimer += Time.deltaTime;

                if (decreaseTimer >= decreaseInterval)
                {
                    thirst += 10;
                    if (thirst > thirstMax)
                    {
                        thirst = thirstMax;
                    }
                    decreaseTimer = 0f;
                }
                break;

            case "mating":
                // Mating behavior
                break;

            case "idle":
                // Pathfinding for random movement 
                break;

            default:
                Debug.LogError("Error: " + organismName + " has an unrecognized behaviouralState. ");
                break;
        }
    }

    public void Detection()
    {
        // Only search for objects every 2 seconds to optimize performance
        if (Time.time % 2f < Time.deltaTime)
        {
            GameObject closestWolf = null;
            float closestDistanceToWolf = Mathf.Infinity;
            GameObject closestPlant = null;
            float closestDistanceToPlant = Mathf.Infinity;
            GameObject closestDeer = null;
            float closestDistanceToMate = Mathf.Infinity;

            // Find all wolves in the scene
            foreach (GameObject wolf in GameObject.FindGameObjectsWithTag("Wolf"))
            {
                // Check distance
                float distance = Vector3.Distance(transform.position, wolf.transform.position);
                if (distance < closestDistanceToWolf && distance <= 100f)
                {
                    closestDistanceToWolf = distance;
                    distanceToWolf = closestDistanceToWolf;
                    closestWolf = wolf;
                }
            }

            // Find all plant in the scene
            foreach (GameObject plant in GameObject.FindGameObjectsWithTag("Plant"))
            {
                // Check distance
                float distance = Vector3.Distance(transform.position, plant.transform.position);
                if (distance < closestDistanceToPlant && distance <= 100f)
                {
                    closestDistanceToPlant = distance;
                    distanceToPlant = closestDistanceToPlant;
                    closestPlant = plant;
                }
            }

            // Find all deer in the scene
            foreach (GameObject deer in GameObject.FindGameObjectsWithTag("Deer"))
            {
                // Skip self and inactive deer
                if (deer == gameObject || !deer.activeInHierarchy)
                {
                    continue;
                }

                // Skip deer that are busy
                DeerOOP potentialMate = deer.GetComponent<DeerOOP>();
                if ((potentialMate != null) && (potentialMate.behaviouralState != "mating" || potentialMate.behaviouralState != "runFromWolf"))
                {
                    continue;
                }

                // Check distance
                float distance = Vector3.Distance(transform.position, deer.transform.position);
                if (distance < closestDistanceToMate && distance <= 100f)
                {
                    closestDistanceToMate = distance;
                    distanceToMate = closestDistanceToMate;
                    closestDeer = deer;
                }
            }
        }
    }

    ////////////////////////////////////

    void RunFromWolf()
    {
        // Only search for wolves every 2 seconds to optimize performance
        if (Time.time % 2f < Time.deltaTime)
        {
            GameObject closestWolf = null;
            float closestDistanceToWolf = Mathf.Infinity;

            // Find all wolves in the scene
            foreach (GameObject wolf in GameObject.FindGameObjectsWithTag("Wolf"))
            {
                // Check distance
                float distance = Vector3.Distance(transform.position, wolf.transform.position);
                if (distance < closestDistanceToWolf && distance <= 100f)
                {
                    closestDistanceToWolf = distance;
                    closestWolf = wolf ;
                }
            }

            // If we found a wolf
            if (closestWolf != null)
            {
                // Check if we're close enough to wolf
                if (closestDistanceToWolf <= agent.stoppingDistance * 1.5f)
                {
                    agent.isStopped = true;
                    return;
                }

                // Move toward plant
                agent.isStopped = false;
                agent.speed = (stamina > staminaMax / 2) ? 5f : 3f; // Faster if high stamina
                agent.SetDestination(closestWolf.transform.position);
            }
            else
            {
                // If no plant found then go idle
                behaviouralState = "idle";
                agent.isStopped = true;
            }
        }
    }


    void SearchForPlant()
    {
        // Only search for plants every 2 seconds to optimize performance
        if (Time.time % 2f < Time.deltaTime)
        {
            GameObject closestPlant = null;
            float closestDistanceToPlant = Mathf.Infinity;

            // Find all plant in the scene
            foreach (GameObject plant in GameObject.FindGameObjectsWithTag("Plant"))
            {
                // Check distance
                float distance = Vector3.Distance(transform.position, plant.transform.position);
                if (distance < closestDistanceToPlant && distance <= 100f)
                {
                    closestDistanceToPlant = distance;
                    closestPlant = plant;
                }
            }

            // If we found a plant
            if (closestPlant != null)
            {
                // Check if we're close enough to plant
                if (closestDistanceToPlant <= agent.stoppingDistance * 1.5f)
                {
                    agent.isStopped = true;
                    return;
                }

                // Move toward plant
                agent.isStopped = false;
                agent.speed = (stamina > staminaMax / 2) ? 5f : 3f; // Faster if high stamina
                agent.SetDestination(closestPlant.transform.position);
            }
            else
            {
                // If no plant found then go idle
                behaviouralState = "idle";
                agent.isStopped = true;
            }
        }
    }


    void SearchForMate()
    {
        // Only search for mates every 2 seconds to optimize performance
        if (Time.time % 2f < Time.deltaTime)
        {
            GameObject closestDeer = null;
            float closestDistanceToMate = Mathf.Infinity;

            // Find all deer in the scene
            foreach (GameObject deer in GameObject.FindGameObjectsWithTag("Deer"))
            {
                // Skip self and inactive deer
                if (deer == gameObject || !deer.activeInHierarchy)
                {
                    continue;
                }

                // Skip deer that are busy
                DeerOOP potentialMate = deer.GetComponent<DeerOOP>();
                if ((potentialMate != null) && (potentialMate.behaviouralState != "mating" || potentialMate.behaviouralState != "runFromWolf"))
                {
                    continue;
                }

                // Check distance
                float distance = Vector3.Distance(transform.position, deer.transform.position);
                if (distance < closestDistanceToMate && distance <= 100f)
                {
                    closestDistanceToMate = distance;
                    closestDeer = deer;
                }
            }

            // If we found a mate
            if (closestDeer != null)
            {
                // Check if we're close enough to mate
                if (closestDistanceToMate <= agent.stoppingDistance * 1.5f)
                {
                    agent.isStopped = true;
                    return;
                }

                // Move toward mate
                agent.isStopped = false;
                if (movementState == "running")
                {
                    agent.speed = 5f;
                }
                else if (movementState == "walking")
                {
                    agent.speed = 3f;
                }

                agent.SetDestination(closestDeer.transform.position);
            }
            else
            {
                // If no mate found then go idle
                behaviouralState = "idle";
            }
        }
    }


    // Method to detect when FreeCamPlayer left clicks on an organism when in range
    public void OnMouseOver()
    {
        GameObject freecamobject = GameObject.Find("FreeCamPlayer");
        float distance = Vector3.Distance(gameObject.transform.position, freecamobject.transform.position);

        if (distance <= 10 && Input.GetMouseButtonDown(0))
        {
            // Set static variables with current deer's info
            selectedDeerName = organismName;
            selectedDeerHealth = health;
            selectedDeerHunger = hunger;
            selectedDeerThirst = thirst;
            selectedDeerStamina = stamina;
            selectedDeerMovementState = movementState;
            selectedDeerBehavioralState = behaviouralState;

            // Set the CanvasOrganismDataUI to true
            CanvasOrganismDataUI = true;
            FreeCamControllerUpdater = true;
        }
    }
}

