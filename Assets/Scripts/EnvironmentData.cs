using UnityEngine;
using UnityEngine.UI;

public class EnvironmentData : MonoBehaviour
{
    // Get starting populatations and time from OrganismInitilisation variables 
    public static int DeerPopulation = OrganismInitilisation.DeerStartingPopulation;
    public static int WolfPopulation = OrganismInitilisation.WolfStartingPopulation;
    public static int PlantPopulation = OrganismInitilisation.PlantStartingPopulation;
    public static float TimeRemaining = OrganismInitilisation.TimeStartingLength;
    private bool isSimulationRunning = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isSimulationRunning)
        {
            // Decrease time remaining by the time passed since the last frame
            TimeRemaining -= Time.deltaTime;

            // Check if the simulation has ended
            if (TimeRemaining <= 0)
            {
                TimeRemaining = 0;
                isSimulationRunning = false;
                Debug.Log("Simulation Ended");
            }
        }
    }


}
