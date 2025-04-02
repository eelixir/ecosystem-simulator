using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUpdater : MonoBehaviour
{
    public GameObject CanvasPauseMenu;
    public GameObject CanvasControlsMenu;
    public bool PauseMenuStatus;
    public static bool FreeCamControllerUpdater;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CanvasPauseMenu.SetActive(false);
        CanvasControlsMenu.SetActive(false);
        PauseMenuStatus = false;
        FreeCamControllerUpdater = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // If escape key pressed then show pause menu
        if (Input.GetKey(KeyCode.Escape))
        {
            CanvasPauseMenu.SetActive(true);
            PauseMenuStatus = true;
            FreeCamControllerUpdater = true;
            Time.timeScale = 0;
        }
    }

    // Unpause game
    public void Unpause()
    {
        CanvasPauseMenu.SetActive(false);
        CanvasControlsMenu.SetActive(false);
        PauseMenuStatus = false;
        FreeCamControllerUpdater = false;
        Time.timeScale = 1;
    }

    // Changes scene to environment
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Changes menu to pause menu
    public void Controls()
    {
        CanvasPauseMenu.SetActive(false);
        PauseMenuStatus = false;
        CanvasControlsMenu.SetActive(true);
    }

    // Returns from controls to pause menu
    public void Return()
    {
        CanvasPauseMenu.SetActive(true);
        CanvasControlsMenu.SetActive(false);
        PauseMenuStatus = false;
    }
}
