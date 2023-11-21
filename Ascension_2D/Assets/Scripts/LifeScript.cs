using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeScript : MonoBehaviour
{
    private bool collected = false;
    public GameObject lifeEnergy;
        

    void Start()
    {
        lifeEnergy = GameObject.FindWithTag("LifeEnergyText");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with the player
        if (other.CompareTag("Player") && !collected)
        {
            // Increment a count (You should manage this count elsewhere)
            GameHandler.lifeEnergyScore++;
            Text lifeText = lifeEnergy.GetComponent<Text>();
            lifeText.text = "" + GameHandler.lifeEnergyScore;

            if (SceneManager.GetActiveScene().name == "Tutorial") {
                Text instructionsText = GameObject.FindWithTag("Instructions").GetComponent<Text>();
                if (GameHandler.lifeEnergyScore == 1) {
                    instructionsText.text = "Use the space bar to jump! Try hopping up onto the platform.";
                    PlayerJump.jumpFrozen = false;
                } else if (GameHandler.lifeEnergyScore == 3) {
                    PlayerHeal.canHealSelf = true;
                    instructionsText.text = "Life Energy can be used on yourself or on an enemy. If you are not at 100% health, using it on yourself will heal you. Try it with [m]!";
                }     
            }

            // Mark the object as collected
            collected = true;

            // Destroy the object
            Destroy(gameObject);
        }
    }
}
