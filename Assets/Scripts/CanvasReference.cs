using UnityEngine;
using UnityEngine.UI;

public class reference : MonoBehaviour
{
    public GameObject CanvasOrganismData;

    // References to the UI Text elements
    public Text organismNameText;
    public Text organismHealthText;
    public Text organismHungerText;
    public Text organismThirstText;
    public Text organismStaminaText;
    public Text organismMovementStateText;
    public Text organismBehavioralStateText;

    void Start()
    {
        // Initial setup, if necessary
    }

    void Update()
    {
        if (DeerOOP.CanvasOrganismDataUI || WolfOOP.CanvasOrganismDataUI)
        {
            // Activate the canvas UI
            CanvasOrganismData.SetActive(true);

            // Update the UI with the selected organism's data
            UpdateUI();
        }
        else
        {
            // Deactivate canvas if neither flag is true
            CanvasOrganismData.SetActive(false);
        }
    }

    // Method to update the UI values once when an organism is selected
    void UpdateUI()
    {
        // If a Deer is selected, update the UI for the deer
        if (DeerOOP.CanvasOrganismDataUI)
        {
            organismNameText.text = DeerOOP.selectedDeerName;
            organismHealthText.text = DeerOOP.selectedDeerHealth.ToString();
            organismHungerText.text = DeerOOP.selectedDeerHunger.ToString();
            organismThirstText.text = DeerOOP.selectedDeerThirst.ToString();
            organismStaminaText.text = DeerOOP.selectedDeerStamina.ToString();
            organismMovementStateText.text = DeerOOP.selectedDeerMovementState;
            organismBehavioralStateText.text = DeerOOP.selectedDeerBehavioralState;
        }
        // If a Wolf is selected, update the UI for the wolf
        else if (WolfOOP.CanvasOrganismDataUI)
        {
            organismNameText.text = WolfOOP.selectedWolfName;
            organismHealthText.text = WolfOOP.selectedWolfHealth.ToString();
            organismHungerText.text = WolfOOP.selectedWolfHunger.ToString();
            organismThirstText.text = WolfOOP.selectedWolfThirst.ToString();
            organismStaminaText.text = WolfOOP.selectedWolfStamina.ToString();
            organismMovementStateText.text = WolfOOP.selectedWolfMovementState;
            organismBehavioralStateText.text = WolfOOP.selectedWolfBehavioralState;
        }
    }

    // Exit method to close the UI and reset relevant flags


    public void Exit()
    {
        DeerOOP.CanvasOrganismDataUI = false;
        DeerOOP.FreeCamControllerUpdater = false;
        WolfOOP.CanvasOrganismDataUI = false;
        WolfOOP.FreeCamControllerUpdater = false;
        CanvasOrganismData.SetActive(false);
        Debug.Log("Organism Data Exitted");
    }

}
