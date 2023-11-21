using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeScript : MonoBehaviour
{
    private bool collected = false;
    public GameObject lifeEnergy;
    public GameObject npc;
        

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
                } else if (GameHandler.lifeEnergyScore == 2) {
                    instructionsText.text = "Watch out! An enemy character has appeared! Use the down arrow or [s] in order to hide from the enemy character.";
                    PlayerHide.canHide = true;
                    npc.SetActive(true);
                }
                    
            }

            // Mark the object as collected
            collected = true;

            // Destroy the object
            Destroy(gameObject);
        }
    }
}
