using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerOOP : OrganismOOP
{
    private float decreaseTimer = 0f;
    private float decreaseInterval = 1f;

    void Start()
    {
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

        Debug.Log(organismName + " initialized.");
    }

    void Update()
    {
        if (isAlive)
        {
            if (health <= 0)
            {
                isAlive = false;
            }

            Move();

            decreaseTimer += Time.deltaTime;

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

                Debug.Log($"Health: {health}, Stamina: {stamina}, Hunger: {hunger}, Thirst: {thirst}");
            }

            if (stamina <= 0)
            {
                movementState = "walking";
                Debug.Log(organismName + " is walking");
            }
            else if (stamina >= 25)
            {
                movementState = "running";
                Debug.Log(organismName + " is running");
            }
        }
    }

    public void PlantSearch()
    {
        Debug.Log(organismName + " is searching for plants.");
    }

    public override void Move()
    {
        Debug.Log(organismName + " is moving.");
    }
}
