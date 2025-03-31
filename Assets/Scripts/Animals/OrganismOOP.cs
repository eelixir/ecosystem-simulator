using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrganismOOP : MonoBehaviour
{
    // Attributes
    public Vector3 position;
    public string organismName;
    public bool isAlive;
    public int health;
    public int healthMax;
    public int hunger;
    public int hungerMax;
    public int thirst;
    public int thirstMax;
    public int stamina;
    public int staminaMax;
    public string movementState;
    public string behaviouralState;
    public string[] skills;
    public float radius;
    public NavMeshAgent agent;


    // Searching for water method
    public void SearchForWater()
    {
        // Only search for water every 2 seconds to optimize performance
        if (Time.time % 2f < Time.deltaTime)
        {
            GameObject closestWater = null;
            float closestDistance = Mathf.Infinity;

            // Find all water in the scene
            foreach (GameObject water in GameObject.FindGameObjectsWithTag("Water"))
            {
                // Check distance
                float distance = Vector3.Distance(transform.position, water.transform.position);
                if (distance < closestDistance && distance <= 100f)
                {
                    closestDistance = distance;
                    closestWater = water;
                }
            }

            // If we found water
            if (closestWater != null)
            {
                // Check if we're close enough to water
                if (closestDistance <= agent.stoppingDistance * 1.5f)
                {
                    behaviouralState = "drinking";
                    agent.isStopped = true;
                    return;
                }

                // Move toward water
                agent.isStopped = false;
                agent.speed = (stamina > staminaMax / 2) ? 5f : 3f; // Faster if high stamina
                agent.SetDestination(closestWater.transform.position);
            }
            else
            {
                // If no water found then go idle
                behaviouralState = "idle";
                agent.isStopped = true;
            }
        }
    }


    // Born method
    public void Born()
    {
        Debug.Log(organismName + " was born.");
    }
}