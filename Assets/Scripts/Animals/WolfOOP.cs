using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfOOP : OrganismOOP
{
    public GameObject WolfCamera;
    public static bool CanvasOrganismDataUI = false;
    public static bool FreeCamControllerUpdater = false;
    private float decreaseTimer = 0f;
    private float decreaseInterval = 1f;
    private float logTimer = 0f;
    private float logInterval = 5f; // Set log frequency to 5 seconds

    public static string selectedWolfName;
    public static int selectedWolfHealth;
    public static int selectedWolfHunger;
    public static int selectedWolfThirst;
    public static int selectedWolfStamina;
    public static string selectedWolfMovementState;
    public static string selectedWolfBehavioralState;

    void Start()
    {
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
        // Checks if organism is alive
        if (isAlive)
        {
            // If organism is not alive then decrease population by 1 and set isAlive to false
            if (health <= 0)
            {
                health = 0;
                EnvironmentData.WolfPopulation -= 1;
                isAlive = false;
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

            Move();

            // Decrease time by the frames that have passed
            decreaseTimer += Time.deltaTime;
            logTimer += Time.deltaTime;

            // Updates variables every second
            if (decreaseTimer >= decreaseInterval)
            {
                if (behaviouralState == "idle")
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

    // Hunting method
    public void Hunt()
    {
        Debug.Log(organismName + " is hunting.");
    }

    // Moving method overrided from parent class
    public override void Move()
    {
        // moving
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

            // Set the CanvasOrganismDataUI flag to true
            CanvasOrganismDataUI = true;
            FreeCamControllerUpdater = true;
        }
    }
}
