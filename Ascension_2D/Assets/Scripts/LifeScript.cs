using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeScript : MonoBehaviour
{
    private bool collected = false;   

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with the player
        if (other.CompareTag("Player") && !collected) {
            if (GameHandler.lifeEnergyScore < GameHandler.maxLifeEnergy) {
                // Increment a count (You should manage this count elsewhere)
                GameHandler.lifeEnergyScore++;
            }

            // Mark the object as collected
            collected = true;

            // Destroy the object
            Destroy(gameObject);

            Image lifeEnergyBar = GameObject.FindWithTag("LifeEnergyBar").GetComponent<Image>();
            lifeEnergyBar.fillAmount = GameHandler.lifeEnergyScore / GameHandler.maxLifeEnergy;
        }
    }
}
