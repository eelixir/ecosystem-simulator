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
        Debug.Log(organismName + " is moving.");
    }

    public void Heal()
    {
        Debug.Log(organismName + " healed to " + health);
    }

    public void SearchWater()
    {
        Debug.Log(organismName + " is searching for water.");
    }

    public void Eat()
    {
        Debug.Log(organismName + " is eating. Hunger level: " + hunger);
    }

    public void Drink()
    {
        Debug.Log(organismName + " is drinking. Thirst level: " + thirst);
    }

    public void Mate()
    {
        Debug.Log(organismName + " is mating.");
    }

    public void Born()
    {
        Debug.Log(organismName + " was born.");
    }

    public void Dead()
    {
        Debug.Log(organismName + " died.");
    }
}
