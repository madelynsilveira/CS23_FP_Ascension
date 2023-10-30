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
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Active scene:" + scene.name);
        
        mainMenu.SetActive(false);
        winScene.SetActive(false);
        loseScene.SetActive(false);

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
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
        // Set static vars
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