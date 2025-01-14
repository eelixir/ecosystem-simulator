using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DeerController script to be attached to GameObject
public class DeerController : MonoBehaviour
{
    private DeerOOP deer;
    private float decreaseTimer = 0f;
    private float decreaseInterval = 1f; // Decrease every 0.5 seconds

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deer = gameObject.AddComponent<DeerOOP>();

        // Initialise deer attributes
        deer.isAlive = true;
        deer.organismName = "Deer"; // add logic for deer to be named eg. Deer1, Deer 2
        deer.health = 100;
        deer.healthMax = 100;
        deer.hunger = 100;
        deer.hungerMax = 100;
        deer.thirst = 100;
        deer.thirstMax = 100;
        deer.stamina = 100;
        deer.staminaMax = 100;
        deer.movementState = "walking";
        deer.behaviouralState = "idle" ;
        deer.position = transform.position;
        deer.radius = 5.0f;

        Debug.Log(deer.organismName + " initialized.");
    }

    void Update()
    {
        if (deer.isAlive)
        {
            if (deer.health == 0)
            {
                deer.isAlive = false;
            }

            deer.Move();

            // Increment timer
            decreaseTimer += Time.deltaTime;

            // Check if the decrease interval has passed
            if (decreaseTimer >= decreaseInterval)
            {

                // Hunger logic
                if (deer.hunger <= 10)
                {
                    deer.health -= 3;
                    deer.stamina -= 3;
                }

                // Thirst logic
                if (deer.thirst <= 10)
                {
                    deer.health -= 3;
                    deer.stamina -= 3;
                }

                // Reset timer
                decreaseTimer = 0f;

                Debug.Log($"Health: {deer.health}, Stamina: {deer.stamina}, Hunger: {deer.hunger}, Thirst: {deer.thirst}");
            }

            // Stamina logic
            if (deer.stamina <= 0)
            {
                deer.movementState = "walking";
                Debug.Log(deer.organismName + " is walking");
            }
            else if (deer.stamina >= 25)
            {
                deer.movementState = "running";
                Debug.Log(deer.organismName + " is running");
            }

            // ADD BEHAVIOURS - ITERATION 4
        }
    }
}
