using UnityEngine;
using UnityEngine.UI;

public class CanvasSimulationDataUpdater : MonoBehaviour
{
    // Text components for each variable
    public Text DeerPopulationText; 
    public Text WolfPopulationText;
    public Text PlantPopulationText;
    public Text TimeCounterText;

    // Update is called once per frame
    void Update()
    {
        // Update the Text UI to show the current population and time remaining, obtaining data from EnvironmentData script
        DeerPopulationText.text = "Deer - " + EnvironmentData.DeerPopulation.ToString();
        WolfPopulationText.text = "Wolf - " + EnvironmentData.WolfPopulation.ToString();
        PlantPopulationText.text = "Plant - " + EnvironmentData.PlantPopulation.ToString();
        TimeCounterText.text = "Time - " + EnvironmentData.TimeRemaining.ToString("F0") + "s"; // Format float to 2 decimal places 
    }
}


