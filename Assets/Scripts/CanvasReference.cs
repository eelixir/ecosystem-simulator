using UnityEngine;
using UnityEngine.UI;

public class reference : MonoBehaviour
{
    public GameObject CanvasOrganismData;
    public GameObject CanvasOrganismDataGenetics;


    // References to the UI Text elements
    public Text organismNameText;
    public Text organismHealthText;
    public Text organismHungerText;
    public Text organismThirstText;
    public Text organismStaminaText;
    public Text organismMovementStateText;
    public Text organismBehavioralStateText;

    public Text organismNameGeneticText;
    public Text organismHealthMaxText;
    public Text organismHungerMaxText;
    public Text organismThirstMaxText;
    public Text organismStaminaMaxText;
    public Text organismSpeedMaxText;
    public Text organismSightMaxText;

    void Update()
    {
        if ((DeerOOP.CanvasOrganismDataUI || WolfOOP.CanvasOrganismDataUI) && !CanvasOrganismDataGenetics.activeSelf)
        {
            // Activate the canvas UI
            CanvasOrganismData.SetActive(true);
            CanvasOrganismDataGenetics.SetActive(false);

            // Update the UI with the selected organism's data
            UpdateUI();
        }
        else
        {
            // Deactivate canvas if not true
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

            organismNameGeneticText.text = DeerOOP.selectedDeerName;
            organismHealthMaxText.text = DeerOOP.selectedDeerHealthMax.ToString();
            organismHungerMaxText.text = DeerOOP.selectedDeerHungerMax.ToString();
            organismThirstMaxText.text = DeerOOP.selectedDeerThirstMax.ToString();
            organismStaminaMaxText.text = DeerOOP.selectedDeerStaminaMax.ToString();
            organismSpeedMaxText.text = DeerOOP.selectedDeerSpeedMax.ToString();
            organismSightMaxText.text = DeerOOP.selectedDeerSightMax.ToString();
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

            organismNameGeneticText.text = WolfOOP.selectedWolfName;
            organismHealthMaxText.text = WolfOOP.selectedWolfHealthMax.ToString();
            organismHungerMaxText.text = WolfOOP.selectedWolfHungerMax.ToString();
            organismThirstMaxText.text = WolfOOP.selectedWolfThirstMax.ToString();
            organismStaminaMaxText.text = WolfOOP.selectedWolfStaminaMax.ToString();
            organismSpeedMaxText.text = WolfOOP.selectedWolfSpeedMax.ToString();
            organismSightMaxText.text = WolfOOP.selectedWolfSightMax.ToString();
        }
    }

    // Exit method to close the UI and reset cursor
    public void Exit()
    {
        DeerOOP.CanvasOrganismDataUI = false;
        DeerOOP.FreeCamControllerUpdater = false;
        WolfOOP.CanvasOrganismDataUI = false;
        WolfOOP.FreeCamControllerUpdater = false;
        CanvasOrganismData.SetActive(false);
        CanvasOrganismDataGenetics.SetActive(false);
        Debug.Log("Organism Data Exitted");
    }

    public void geneticMenuEnter()
    {
        CanvasOrganismDataGenetics.SetActive(true);
        CanvasOrganismData.SetActive(false);
    }

    public void geneticMenuExit()
    {
        CanvasOrganismDataGenetics.SetActive(false);
        CanvasOrganismData.SetActive(true);
    }

}

