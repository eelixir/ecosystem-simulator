using UnityEngine;
using UnityEngine.AI;

public class OrganismInitilisation : MonoBehaviour
{
    public GameObject DeerPrefab;
    public GameObject WolfPrefab;
    public GameObject PlantPrefab;

    public int DeerCount = 10;
    public int WolfCount = 10;
    public int PlantCount = 10;

    public float cubeHeightOffset = 0f;
    private Collider platformCollider;

    public float minX = 5f, maxX = 95f;
    public float minY = 1f, maxY = 1f;
    public float minZ = 5f, maxZ = 95f;

    void Start()
    {
        // Find the Platform object by its name and get its collider
        GameObject platform = GameObject.Find("Platform");
        if (platform == null)
        {
            Debug.LogError("Platform object not found in the scene!");
            return;
        }

        platformCollider = platform.GetComponent<Collider>();
        if (platformCollider == null)
        {
            Debug.LogWarning("No collider found on the Platform. Adding BoxCollider...");
            platformCollider = platform.AddComponent<BoxCollider>(); // Add BoxCollider if missing
        }

        SpawnObjects(DeerPrefab, DeerCount);
        SpawnObjects(WolfPrefab, WolfCount);
        SpawnObjects(PlantPrefab, PlantCount);
    }

    void SpawnObjects(GameObject objectPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomPositionOnCube();
            if (spawnPosition != Vector3.zero) // Ensure we got a valid position
            {
                Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomPositionOnCube()
    {
        // Get the platform's bounds
        Vector3 platformCenter = platformCollider.bounds.center;
        Vector3 platformSize = platformCollider.bounds.size;

        // Generate random positions within the platform's bounds
        float x = Random.Range(platformCenter.x - platformSize.x / 2, platformCenter.x + platformSize.x / 2);
        float z = Random.Range(platformCenter.z - platformSize.z / 2, platformCenter.z + platformSize.z / 2);

        // Set y to a constant value of 1
        float y = 1f;

        return new Vector3(x, y, z);
    }
}
