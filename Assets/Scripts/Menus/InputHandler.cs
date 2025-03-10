using UnityEngine;
using TMPro;
using UnityEditor.Overlays;

public class InputHandler : MonoBehaviour
{
    // Store the inputed values as strings
    public static string DeerStartPopulation;
    public static string WolfStartPopulation;
    public static string PlantStartPopulation;
    public static string TimeStartLength;

    void Awake()
    {
        // Makes it so the object the script is assigned to isn't destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
    }

    public void OnInputFinished(string value)
    {
        // Remove spaces from the input
        value = value.Trim();

        // Assigns input data to the corresponding variables
        string fieldName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        switch (fieldName)
        {
            case "Field1": DeerStartPopulation = value; break;
            case "Field2": WolfStartPopulation = value; break;
            case "Field3": PlantStartPopulation = value; break;
            case "Field4": TimeStartLength = value; break;
        }

        Debug.Log($"{fieldName}: {value}");
    }
}

