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
    public static bool level1Complete = false;
    public GameObject level2Button;
    public static bool level2Complete = false;
    public GameObject level3Button;
    public static bool level3Complete = false;
    public GameObject level4Button;
    public static bool level4Complete = false;
    public GameObject level5Button;
    public static bool level5Complete = false;
    public GameObject level6Button;
    public static bool level6Complete = false;
    public GameObject level7Button;
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

            if (level1Complete) {
                level2Button.SetActive(true);
            } else {
                level2Button.SetActive(false);
            }
            if (level2Complete) {
                level3Button.SetActive(true);
            } else {
                level3Button.SetActive(false);
            }
            if (level3Complete) {
                level4Button.SetActive(true);
            } else {
                level4Button.SetActive(false);
            }
            if (level4Complete) {
                level5Button.SetActive(true);
            } else {
                level5Button.SetActive(false);
            }
            if (level5Complete) {
                level6Button.SetActive(true);
            } else {
                level6Button.SetActive(false);
            }
            if (level6Complete) {
                level7Button.SetActive(true);
            } else {
                level7Button.SetActive(false);
            }
        } else if (scene.name == "winScene") {
            winScene.SetActive(true);
        } else if (scene.name == "loseScene") {
            loseScene.SetActive(true);
        } else if (scene.name == "Level1" || scene.name == "work_Silas" || scene.name == "Tutorial") {
            playScene.SetActive(true);
            lifeEnergyScore = 0f;
            maxLifeEnergy = 10f;
            lifeEnergyBar.GetComponent<Image>().fillAmount = lifeEnergyScore / maxLifeEnergy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
            // Set static vars
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene("Level1");
            // Set static vars
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene("Level2");
            // Set static vars
    }

    public void PlayLevel3()
    {
        SceneManager.LoadScene("Level3");
            // Set static vars
    }

    public void PlayLevel4()
    {
        SceneManager.LoadScene("Level4");
            // Set static vars
    }

    public void PlayLevel5()
    {
        SceneManager.LoadScene("Level5");
            // Set static vars
    }

    public void PlayLevel6()
    {
        SceneManager.LoadScene("Level6");
            // Set static vars
    }

    public void PlayLevel7()
    {
        SceneManager.LoadScene("Level7");
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
