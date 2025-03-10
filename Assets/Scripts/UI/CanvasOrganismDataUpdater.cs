using UnityEngine;
using UnityEngine.UI;

public class CanvasOrganismDataUpdater : MonoBehaviour
{
    // Text components for each variable
    public Text OrganismNameValue;
    public Text HealthValue;
    public Text HungerValue;
    public Text ThirstValue;
    public Text StaminaValue;
    public Text MovementValue;
    public Text BehaviourValue;


    // Update is called once per frame
    void Update()
    {

    }

    public void UIOrganismDataDisplay(string organismName, int health, int hunger, int thirst, int stamina, string movementState, string behaviouralState)
    {
        // Update the Text UI to show the current population and time remaining, obtaining data from EnvironmentData script
        OrganismNameValue.text = organismName.ToString();
        HealthValue.text = health.ToString();
        HungerValue.text = hunger.ToString();
        ThirstValue.text = thirst.ToString();
        StaminaValue.text = stamina.ToString();
        MovementValue.text = movementState.ToString();
        BehaviourValue.text = behaviouralState.ToString();
    }
}


