using UnityEngine;

public class PlantOOP : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Detection();
    }
    
    // Function to delete plant if deer is eating it
    public void Detection()
    {
        // Detect the closest deer
        foreach (GameObject deer in GameObject.FindGameObjectsWithTag("Deer"))
        {
            float distance = Vector3.Distance(transform.position, deer.transform.position);

            // Destroy if colliding with a deer
            if (distance <= 1f)
            {
                EnvironmentData.PlantPopulation -= 1;
                Object.Destroy(gameObject); 
                return;
            }
        }
    }
}
