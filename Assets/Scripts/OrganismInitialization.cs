using UnityEngine;
using UnityEngine.AI;

public class OrganismInitilisation : MonoBehaviour
{
    // Prefabs for each organism
    public GameObject DeerPrefab;
    public GameObject WolfPrefab;
    public GameObject PlantPrefab;

    // Starting populations and time
    public static int DeerStartingPopulation = 10;
    public static int WolfStartingPopulation = 10;
    public static int PlantStartingPopulation = 10;
    public static float TimeStartingLength = 300;  

    public Collider platformCollider;
    private float timeRemaining;
    private bool isSimulationRunning = true;

    // Randomly spawning new plants
    private float spawnTimer = 0f;
    private float spawnInterval = 5;

    void Start()
    {
         // Find the platform object and its collider
        GameObject platform = GameObject.Find("Platform");
        if (platform == null)
        {
            Debug.LogError("Platform object not found");
            return;
        }

        platformCollider = platform.GetComponent<Collider>();
        if (platformCollider == null)
        {
            Debug.LogError("Platform Collider not found");
            return;
        }

        // Initialize the simulation time
        timeRemaining = TimeStartingLength;

        // Spawn the initial organisms
        SpawnObjects(DeerPrefab, DeerStartingPopulation);
        SpawnObjects(WolfPrefab, WolfStartingPopulation);
        SpawnObjects(PlantPrefab, PlantStartingPopulation);
    }

    void Update()
    {
        if (isSimulationRunning)
        {
            // Decrease time remaining by the time passed since the last frame
            timeRemaining -= Time.deltaTime;

            // Check if the simulation has ended
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isSimulationRunning = false;
                Debug.Log("Simulation Ended");
            }

            // Random chance to spawn a plant every 1 second
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                int randomNum = Random.Range(0, 5);
                if (randomNum == 0)
                {
                    SpawnObjects(PlantPrefab, 1);
                    spawnTimer = 0;
                }
            }

        }
    }

    // Method that spawns in the organisms
    void SpawnObjects(GameObject objectPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomPositionOnPlatform();
            // Check if position is valid
            if (spawnPosition != Vector3.zero)
            {
                Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    // Function that gets a random position on the platform
    Vector3 GetRandomPositionOnPlatform()
    {
        if (platformCollider == null)
        {
            Debug.LogError("Platform Collider is null");
            return Vector3.zero;
        }

        // Get the platform object's bounds
        Vector3 platformCenter = platformCollider.bounds.center;
        Vector3 platformSize = platformCollider.bounds.size;

        // Generate random positions within the platform's bounds
        float x = Random.Range(platformCenter.x - platformSize.x / 2, platformCenter.x + platformSize.x / 2);
        float z = Random.Range(platformCenter.z - platformSize.z / 2, platformCenter.z + platformSize.z / 2);
        float y = 0;

        return new Vector3(x, y, z);
    }
}
