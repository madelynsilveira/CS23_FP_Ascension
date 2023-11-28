using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public Scene scene;
    public GameObject mainMenu;
    public GameObject winScene;
    public GameObject loseScene;
    public GameObject playScene;
    public GameObject lifeEnergyBar;
    public static float lifeEnergyScore;
    public static float maxLifeEnergy;
    public static bool tutorialComplete;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        
        mainMenu.SetActive(false);
        winScene.SetActive(false);
        loseScene.SetActive(false);
        playScene.SetActive(false);

        // Set active menu
        if (scene.name == "MainMenu") {
            mainMenu.SetActive(true);
        } else if (scene.name == "winScene") {
            winScene.SetActive(true);
        } else if (scene.name == "loseScene") {
            loseScene.SetActive(true);
        } else if (scene.name == "Level1" || scene.name == "work_Silas" || scene.name == "Tutorial") {
            playScene.SetActive(true);
            lifeEnergyScore = 0f;
            maxLifeEnergy = 10f;
            lifeEnergyBar.GetComponent<Image>().fillAmount = lifeEnergyScore / maxLifeEnergy;
            if (scene.name == "Tutorial") {
                // PlayerJump.jumpFrozen = true;
                // PlayerHide.canHide = false;
                // PlayerHeal.canHeal = false;
                // GameObject.FindWithTag("NPC").SetActive(false);
                tutorialComplete = false;
            } else {
                // PlayerJump.jumpFrozen = false;
                // PlayerHide.canHide = true;
                // PlayerHeal.canHeal = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        if (!tutorialComplete) {
            SceneManager.LoadScene("Tutorial");
        } else {
            SceneManager.LoadScene("Level1");
        }
        
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
