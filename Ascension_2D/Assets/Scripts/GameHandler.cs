using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public Scene scene;
    public GameObject mainMenu;
    public GameObject winScene;
    public GameObject loseScene;
    public GameObject pauseMenu;
    public GameObject timer;
    public GameObject lifeEnergy;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        
        mainMenu.SetActive(false);
        winScene.SetActive(false);
        loseScene.SetActive(false);
        pauseMenu.SetActive(false);
        timer.SetActive(false);
        lifeEnergy.SetActive(false);

        // Set active menu
        if (scene.name == "MainMenu") {
            mainMenu.SetActive(true);
        } else if (scene.name == "winScene") {
            winScene.SetActive(true);
        } else if (scene.name == "loseScene") {
            loseScene.SetActive(true);
        } else if (scene.name == "Level1" || scene.name == "work_Silas") {
            timer.SetActive(true);
            lifeEnergy.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
            // Set static vars
    }

    public void RestartGame() {
            Time.timeScale = 1f;
            GameHandler_PauseMenu.GameisPaused = false;
            SceneManager.LoadScene("MainMenu");
                // Please also reset all static variables here, for new games!
      }
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
