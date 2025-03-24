using UnityEngine;
using UnityEngine.SceneManagement;

public class reference : MonoBehaviour
{
    public GameObject CanvasOrganismData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (DeerOOP.CanvasOrganismDataUI == true || WolfOOP.CanvasOrganismDataUI == true)
        {
            CanvasOrganismData.SetActive(true);
        }
    }

    public void Exit()
    {
        DeerOOP.CanvasOrganismDataUI = false;
        DeerOOP.FreeCamControllerUpdater = false;   
        WolfOOP.CanvasOrganismDataUI = false;
        WolfOOP.FreeCamControllerUpdater = false;
        CanvasOrganismData.SetActive(false);
        Debug.Log("Organism Data Exitted");
    }

    public void Control()
    {

    }
}
