using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfOOP : OrganismOOP
{
    public GameObject WolfCamera;
    private float decreaseTimer = 0f;
    private float decreaseInterval = 1f;
    private float logTimer = 0f;
    private float logInterval = 1f; // Set log frequency to 1 seconds

    // Organism Data UI
    public static bool CanvasOrganismDataUI = false;
    public static bool FreeCamControllerUpdater = false;
    public static string selectedWolfName;
    public static int selectedWolfHealth;
    public static int selectedWolfHunger;
    public static int selectedWolfThirst;
    public static int selectedWolfStamina;
    public static string selectedWolfMovementState;
    public static string selectedWolfBehavioralState;

    // Pathfinding
    public LayerMask detectionLayer;
    private bool deerDetected = false;
    private bool mateDetected = false;
    private bool waterDetected = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Declare variables for the organism
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
        DetectSurroundings();
        BehaviourUpdating();
        Pathfinding();

        // Checks if organism is alive
        if (isAlive)
        {
            // If organism is not alive then decrease population by 1 and set isAlive to false
            if (health <= 0)
            {
                health = 0;
                EnvironmentData.WolfPopulation -= 1;
                isAlive = false;
                Destroy(gameObject);
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

            // Updates variables every second
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

            // Log stats at the specified interval (5 seconds)
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


    // Checks to see if an object with the tags Deer, Mate or Water are in the radius of the current gameObject
    void DetectSurroundings()
    {
        // Resets detection states
        deerDetected = false;
        mateDetected = false;
        waterDetected = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, detectionLayer);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Deer"))
            {
                deerDetected = true;
            }
            else if (col.CompareTag("Mate"))
            {
                mateDetected = true;
            }
            else if (col.CompareTag("Water"))
            {
                waterDetected = true;
            }
        }
    }


    // Updated the Behavioural State depending on certain conditions
    void BehaviourUpdating()
    {
        if ((hunger <= (hungerMax / 2)) && (hunger < thirst))
        {
            behaviouralState = "hunting";
        }
        else if ((thirst <= (thirstMax / 2)) && (thirst < hunger))
        {
            behaviouralState = "searchWater";
        }
        else if ((thirst <= (thirstMax / 2)) && (hunger <= (hungerMax / 2)) && (thirst == hunger))
        {
            behaviouralState = "searchWater";
        }
        else if (waterDetected)
        {
            behaviouralState = "drinking";
        }
        else if ((waterDetected) && (deerDetected))
        {
            if (thirst <= hunger)
            {
                behaviouralState = "drinking";
            }
            else
            {
                behaviouralState = "hunting";
            }
        }
        else if ((hunger > (hungerMax / 2)) && (thirst > (thirstMax / 2)) && (stamina > (staminaMax / 2)))
        {
            behaviouralState = "searchMate";
        }
        else if ((mateDetected) && (hunger > (hungerMax / 2)) && (thirst > (thirstMax / 2)) && (stamina > (staminaMax / 2)))
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
            case "hunting":
                // Pathfinding to find deer
                SearchForDeer();
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
                break;

            case "drinking":
                // Drinking behavior
                break;

            case "mating":
                // Mating behavior
                break;

            case "idle":
                // Pathfinding for random movement
                break;

            default:
                Debug.LogError("Error: " + organismName + " has an unrecognized behaviouralState.");
                break;
        }
    }


    void SearchForDeer()
    {
        // Only search for deer every 2 seconds to optimize performance
        if (Time.time % 2f < Time.deltaTime)
        {
            GameObject closestDeer = null;
            float closestDistance = Mathf.Infinity;

            // Find all deer in the scene
            foreach (GameObject deer in GameObject.FindGameObjectsWithTag("Deer"))
            {
                // Check distance
                float distance = Vector3.Distance(transform.position, deer.transform.position);
                if (distance < closestDistance && distance <= 100f)
                {
                    closestDistance = distance;
                    closestDeer = deer;
                }
            }

            // If we found deer
            if (closestDeer != null)
            {
                // Check if we're close enough to deer
                if (closestDistance <= agent.stoppingDistance * 1.5f)
                {
                    behaviouralState = "eating";
                    agent.isStopped = true;
                    return;
                }

                // Move toward deer
                agent.isStopped = false;
                agent.speed = (stamina > staminaMax / 2) ? 5f : 3f; // Faster if high stamina
                agent.SetDestination(closestDeer.transform.position);
            }
            else
            {
                // If no deer found then go idle
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
            GameObject closestWolf = null;
            float closestDistance = Mathf.Infinity;

            // Find all wolf in the scene
            foreach (GameObject wolf in GameObject.FindGameObjectsWithTag("Wolf"))
            {
                // Skip self and inactive wolf
                if (wolf == gameObject || !wolf.activeInHierarchy)
                {
                    continue;
                }

                // Skip wolf that are busy
                WolfOOP potentialMate = wolf.GetComponent<WolfOOP>();
                if ((potentialMate != null) && (potentialMate.behaviouralState == "mating"))
                {
                    continue;
                }

                // Check distance
                float distance = Vector3.Distance(transform.position, wolf.transform.position);
                if (distance < closestDistance && distance <= 100f) 
                {
                    closestDistance = distance;
                    closestWolf = wolf;
                }
            }

            // If we found a mate
            if (closestWolf != null)
            {
                // Check if we're close enough to mate
                if (closestDistance <= agent.stoppingDistance * 1.5f)
                {
                    behaviouralState = "mating";
                    agent.isStopped = true;
                    return;
                }

                // Move toward mate
                agent.isStopped = false;
                agent.speed = (stamina > staminaMax / 2) ? 5f : 3f; // Faster if high stamina
                agent.SetDestination(closestWolf.transform.position);
            }
            else
            {
                // If no mate found then go idle
                behaviouralState = "idle";
                agent.isStopped = true;
            }
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
}
