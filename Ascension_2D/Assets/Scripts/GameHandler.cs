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
    bool pauseMenuActive;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Active scene:" + scene.name);
        
        mainMenu.SetActive(false);
        winScene.SetActive(false);
        loseScene.SetActive(false);
        pauseMenu.SetActive(false);
        pauseMenuActive = false;

        if (scene.name == "MainMenu") {
            mainMenu.SetActive(true);
        } else if (scene.name == "winScene") {
            winScene.SetActive(true);
        } else if (scene.name == "loseScene") {
            loseScene.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scene.name == "Level1") {
            if (Input.GetKey("escape")) {
                if (pauseMenuActive) {
                    pauseMenu.SetActive(false);
                    pauseMenuActive = false;
                } else {
                    pauseMenu.SetActive(true);
                    pauseMenuActive = true;
                }
            }
        }
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
