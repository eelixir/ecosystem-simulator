using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DeerOOP;

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




// Sub-class for wolf inherited from OrganismOOP class
public class DeerOOP : OrganismOOP
{
    private DeerOOP deer;
    private float decreaseTimer = 0f;
    private float decreaseInterval = 1f; // Decrease every 1 seconds

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
        deer.behaviouralState = "idle";
        deer.position = transform.position;
        deer.radius = 5.0f;

        Debug.Log(deer.organismName + " initialized.");
    }

    void Update()
    {
        if (deer.isAlive)
        {
            if (deer.health <= 0)
            {
                deer.isAlive = false;
            }

            deer.Move();

            // Increment timer
            decreaseTimer += Time.deltaTime;

            // Check if the decrease interval has passed
            if (decreaseTimer >= decreaseInterval)
            {
                // Nautrally decrease hunger and thrist every 1 second
                if (deer.behaviouralState == "idle")
                {
                    deer.hunger -= 1;
                    deer.thirst -= 1;
                }

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

            public void Hunt()
            {
                // add logic for hunting - ITERATION 4
                Debug.Log(organismName + " is hunting.");
            }
            public override void Move()
            {
                // add code for deer movement
                Debug.Log(organismName + " is moving.");
            }
        }
    }
}



// Sub-class for deer inherited from OrganismOOP class
public class WolfOOP : OrganismOOP
{
    private wolfOOP wolf;
    private float decreaseTimer = 0f;
    private float decreaseInterval = 1f; // Decrease every 1 seconds

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wolf = gameObject.AddComponent<wolfOOP>();

        // Initialise wolf attributes
        wolf.isAlive = true;
        wolf.organismName = "wolf"; // add logic for wolf to be named eg. wolf1, wolf 2
        wolf.health = 100;
        wolf.healthMax = 100;
        wolf.hunger = 100;
        wolf.hungerMax = 100;
        wolf.thirst = 100;
        wolf.thirstMax = 100;
        wolf.stamina = 100;
        wolf.staminaMax = 100;
        wolf.movementState = "walking";
        wolf.behaviouralState = "idle";
        wolf.position = transform.position;
        wolf.radius = 5.0f;

        Debug.Log(wolf.organismName + " initialized.");
    }

    void Update()
    {
        if (wolf.isAlive)
        {
            if (wolf.health <= 0)
            {
                wolf.isAlive = false;
            }

            wolf.Move();

            // Increment timer
            decreaseTimer += Time.deltaTime;

            // Check if the decrease interval has passed
            if (decreaseTimer >= decreaseInterval)
            {
                // Nautrally decrease hunger and thrist every 1 second
                if (wolf.behaviouralState == "idle")
                {
                    wolf.hunger -= 1;
                    wolf.thirst -= 1;
                }

                // Hunger logic
                if (wolf.hunger <= 10)
                {
                    wolf.health -= 3;
                    wolf.stamina -= 3;
                }

                // Thirst logic
                if (wolf.thirst <= 10)
                {
                    wolf.health -= 3;
                    wolf.stamina -= 3;
                }

                // Reset timer
                decreaseTimer = 0f;

                Debug.Log($"Health: {wolf.health}, Stamina: {wolf.stamina}, Hunger: {wolf.hunger}, Thirst: {wolf.thirst}");
            }

            // Stamina logic
            if (wolf.stamina <= 0)
            {
                wolf.movementState = "walking";
                Debug.Log(wolf.organismName + " is walking");
            }
            else if (wolf.stamina >= 25)
            {
                wolf.movementState = "running";
                Debug.Log(wolf.organismName + " is running");
            }

            // ADD BEHAVIOURS - ITERATION 4

            public void plantSearch
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
    }
}