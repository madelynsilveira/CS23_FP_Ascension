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
    private float startingLifeEnergy;
    public static float maxLifeEnergy;
    public static int soulsHealed;
    private int startingSoulsHealed;

    // public GameObject tutorialStar;
    // public static bool tutorialStarred = false;

    public static bool level1Complete = false;
    public GameObject level1Star;
    public static bool level1Starred = false;

    public GameObject level2Button;
    public static bool level2Complete = false;
    public GameObject level2Star;
    public static bool level2Starred = false;

    public GameObject level3Button;
    public static bool level3Complete = false;
    public GameObject level3Star;
    public static bool level3Starred = false;

    public GameObject level4Button;
    public static bool level4Complete = false;
    public GameObject level4Star;
    public static bool level4Starred = false;

    public GameObject level5Button;
    public static bool level5Complete = false;
    public GameObject level5Star;
    public static bool level5Starred = false;

    public GameObject level6Button;
    public static bool level6Complete = false;
    public GameObject level6Star;
    public static bool level6Starred = false;

    public AudioSource earlyAudio;
    public AudioSource middleAudio;
    public AudioSource lateAudio;

    public static string previousScene;


    void Start()
    {
        scene = SceneManager.GetActiveScene();
        
        mainMenu.SetActive(false);
        winScene.SetActive(false);
        loseScene.SetActive(false);
        playScene.SetActive(false);

        // Set active menu
        if (scene.name == "MainMenu") {
            earlyAudio.Play();

            mainMenu.SetActive(true);

            // adjust button colors
            level2Button.SetActive(level1Complete);
            level3Button.SetActive(level2Complete);
            level4Button.SetActive(level3Complete);
            level5Button.SetActive(level4Complete);
            level6Button.SetActive(level5Complete);

            // adjust stars
            level1Star.SetActive(level1Starred);
            level2Star.SetActive(level2Starred);
            level3Star.SetActive(level3Starred);
            level4Star.SetActive(level4Starred);
            level5Star.SetActive(level5Starred);
            level6Star.SetActive(level6Starred);


        } else if (scene.name == "winScene") {
            earlyAudio.Play();
            winScene.SetActive(true);
            if (soulsHealed == 1) {
                GameObject.FindWithTag("SoulsHealed").GetComponent<Text>().text = "You healed 1 soul. Good job.";
            } else if (soulsHealed == 0) {
                GameObject.FindWithTag("SoulsHealed").GetComponent<Text>().text = "You didn't heal any souls. That was selfish of you.";
            } else {
                GameObject.FindWithTag("SoulsHealed").GetComponent<Text>().text = "You healed " + soulsHealed + " souls! Great job!";
            }

            bool[] stars = {
                level1Starred,
                level2Starred,
                level3Starred,
                level4Starred,
                level5Starred,
                level6Starred
            };

            int numStars = 0;
            foreach (bool star in stars) {
                if (star) {
                    numStars++;
                }
            }
            GameObject.FindWithTag("NumStars").GetComponent<Text>().text = "You got " + numStars + " / 6 possible stars";
        } else if (scene.name == "loseScene") {
            loseScene.SetActive(true);
            earlyAudio.Play();
        } else {
            playScene.SetActive(true);
            if (scene.name == "Tutorial") {
                lifeEnergyScore = 0f;
                soulsHealed = 0;
            }

            startingLifeEnergy = lifeEnergyScore;
            startingSoulsHealed = soulsHealed;
            
            if (scene.name == "Tutorial" || scene.name == "Level1" || scene.name == "Level2") {
                earlyAudio.Play();
            } else if (scene.name == "Level3" || scene.name == "Level4") {
                middleAudio.Play();
            } else {
                lateAudio.Play();
            }

            maxLifeEnergy = 10f;
            lifeEnergyBar.GetComponent<Image>().fillAmount = lifeEnergyScore / maxLifeEnergy;
        }
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

    public void RestartGame() {
            Time.timeScale = 1f;
            GameHandler_PauseMenu.GameisPaused = false;
            // Please also reset all static variables here, for new games!
            lifeEnergyScore = startingLifeEnergy;
            soulsHealed = startingSoulsHealed;
            if (scene.name == "loseScene") {
                SceneManager.LoadScene(previousScene);
            } else {
                SceneManager.LoadScene(scene.name);
            }
    }

    public void RestartGameAfterWin() {
            Time.timeScale = 1f;
            GameHandler_PauseMenu.GameisPaused = false;

            lifeEnergyScore = startingLifeEnergy;
            soulsHealed = startingSoulsHealed;
            
            // adjust button colors
            level2Button.SetActive(false);
            level3Button.SetActive(false);
            level4Button.SetActive(false);
            level5Button.SetActive(false);
            level6Button.SetActive(false);

            // adjust stars
            level1Star.SetActive(false);
            level2Star.SetActive(false);
            level3Star.SetActive(false);
            level4Star.SetActive(false);
            level5Star.SetActive(false);
            level6Star.SetActive(false);



            SceneManager.LoadScene("MainMenu");
            
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void MainMenu()
    {
            Time.timeScale = 1f;
            GameHandler_PauseMenu.GameisPaused = false;
            SceneManager.LoadScene("MainMenu");
                // Please also reset all static variables here, for new games!
                lifeEnergyScore = 0f;
                soulsHealed = 0;
      }
}
