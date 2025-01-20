using UnityEngine;
using TMPro;

public class InputHandler : MonoBehaviour
{
    // Store the collected values
    private string DeerPopulationInput;
    private string WolfPopulationInput;
    private string PlantPopulationInput;
    private string TimeLengthInput;

    // Called when any input field finishes editing
    public void OnInputFinished(string value)
    {
        // Get the name of the input field that just finished
        string fieldName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        // Store value based on field name
        switch (fieldName)
        {
            case "Field1":
                DeerPopulationInput = value;
                break;
            case "Field2":
                WolfPopulationInput = value;
                break;
            case "Field3":
                PlantPopulationInput = value;
                break;
            case "Field4":
                TimeLengthInput = value;
                break;
        }

        Debug.Log($"{fieldName}: {value}");
    }
}


