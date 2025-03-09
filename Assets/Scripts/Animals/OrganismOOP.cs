using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Methods
    public virtual void Move()
    {
        // Moving
    }

    // Healing method
    public void Heal()
    {
        Debug.Log(organismName + " healed to " + health);
    }

    // Searching for water method
    public void SearchWater()
    {
        Debug.Log(organismName + " is searching for water.");
    }

    // Eating method
    public void Eat()
    {
        Debug.Log(organismName + " is eating. Hunger level: " + hunger);
    }

    // Drinking method
    public void Drink()
    {
        Debug.Log(organismName + " is drinking. Thirst level: " + thirst);
    }

    // Mating method
    public void Mate()
    {
        Debug.Log(organismName + " is mating.");
    }

    // Born method
    public void Born()
    {
        Debug.Log(organismName + " was born.");
    }

    // Dead method
    public void Dead()
    {
        Debug.Log(organismName + " died.");
    }
}
