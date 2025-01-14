using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganismOOP : MonoBehaviour
{
    // Attributes
    public Vector3 position;
    public string organismName; // changed from name -> organismName since Unity's object class already has a name property that refers to the name of the GameObject
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
        // movement logic
        Debug.Log(organismName + " is moving.");
    }

    public void Heal()
    {
        // healing logic - ITERATION 4
        Debug.Log(organismName + " healed to " + health);
    }
    
    public void SearchWater()
    {
        // searching for water logic - ITERATION 4
        Debug.Log(organismName + " is searching for water.");
    }

    public void Eat()
    {
        // eating logic - ITERATION 4
        Debug.Log(organismName + " is eating. Hunger level: " + hunger);
    }

    public void Drink()
    {
        // drinking logic - ITERATION 4
        Debug.Log(organismName + " is drinking. Thirst level: " + thirst);
    }

    public void Mate()
    {
        // mating logic - ITERATION 4
        Debug.Log(organismName + " is mating.");
    }

    public void Born()
    {
        // birth logic
        Debug.Log(organismName + " was born.");
    }

    public void Dead()
    {
        // death logic - ITERATION 4
        Debug.Log(organismName + " died.");
    }
}


// Sub-class for deer inherited from OrganismOOP class
public class DeerOOP : OrganismOOP
{
    public void SearchPlant()
    {
        // add searching plant code - ITERATION 4
        Debug.Log(organismName + " is searching for plants.");

    }

    public override void Move()
    {
        // add code for deer movement
        Debug.Log(organismName + " is moving.");

    }
}


// Sub-class for wolf inherited from OrganismOOP class
public class WolfOOP : OrganismOOP
{
    public void Hunt()
    {
        // add logic for hunting - ITERATION 4
        Debug.Log(organismName + " is hunting.");
    }
}