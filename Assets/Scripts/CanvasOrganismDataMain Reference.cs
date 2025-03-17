using UnityEngine;

public class CanvasReference : MonoBehaviour
{
    public  GameObject CanvasOrganismData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DeerOOP.CanvasOrganismDataUI == true)
        {
            CanvasOrganismData.SetActive(true);
        }
    }
}
