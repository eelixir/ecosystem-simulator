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

    // End Screen UI Values
    public Text totalPopulationText;
    public Text finalDeerPopulationText;
    public Text finalWolfPopulationText;
    public Text finalSimulationTimeText;

    // Get end screen menu canvas 
    public GameObject CanvasEndScreen;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CanvasEndScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSimulationRunning)
        {
            // Decrease time remaining by the time passed since the last frame
            TimeRemaining -= Time.deltaTime;

            // Check if the simulation has ended
            if (TimeRemaining <= 0 || DeerPopulation == 0 || WolfPopulation == 0)
            {
                TimeRemaining = 0;
                isSimulationRunning = false;
                Debug.Log("Simulation Ended");
                // Changes UI to end screen
                CanvasEndScreen.SetActive(true);

                // Set UI text values to current variable values
                totalPopulationText.text = DeerPopulation.ToString();
                finalDeerPopulationText.text = WolfPopulation.ToString();
                finalWolfPopulationText.text = WolfPopulation.ToString();
                finalSimulationTimeText.text = TimeRemaining.ToString();

            }
        }
    }
}


