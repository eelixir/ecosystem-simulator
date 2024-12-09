using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Changes scene to Simulation Initiation Menu
    public void GameStart()
    {
        SceneManager.LoadScene("SimulationInitiation");
    }

    // Changes scene to Controls Menu
    public void Controls()
    {
        SceneManager.LoadScene("ControlsMenu");
    }

    // Changes scene to Main Menu
    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Changes scene to Pause Menu
    public void Pause()
    {
        SceneManager.LoadScene("PauseMenu");
    }

    // Changes scene to Environment
    public void Unpause()
    {
        SceneManager.LoadScene("Environment");
    }

    // Closes the application
    public void Exit()
    {
        Application.Quit();
    }
}








