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
    public int speedMax;
    public string movementState;
    public string behaviouralState;
    public float radius;

    // Pathfinding 
    public NavMeshAgent agent;


    // Born method
    public void Born()
    {
        Debug.Log(organismName + " was born.");
    }
}