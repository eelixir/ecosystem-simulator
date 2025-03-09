using UnityEngine;
using UnityEngine.UI;

public class CanvasUpdater : MonoBehaviour
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
        DeerPopulationText.text = "Deer Population - " + EnvironmentData.DeerPopulation.ToString();
        WolfPopulationText.text = "Wolf Population - " + EnvironmentData.WolfPopulation.ToString();
        PlantPopulationText.text = "Plant Population - " + EnvironmentData.PlantPopulation.ToString();
        TimeCounterText.text = "Time Remaining - " + EnvironmentData.TimeRemaining.ToString("F0") + "s"; // Format float to 2 decimal places 
    }
}


