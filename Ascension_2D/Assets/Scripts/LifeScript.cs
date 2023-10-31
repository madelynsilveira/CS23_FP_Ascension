using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    private bool collected = false;
    public GameObject lifeTimer;
        

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with the player
        // if (other.CompareTag("Player") && !collected)
        // {
        //     // Increment a count (You should manage this count elsewhere)
        //     LifeEnergyScore++;
        //     Text lifeTime = lifeTimer.GetComponent<Text>();
        //     lifeTime.text = "" + lifeTimer;

        //     // Mark the object as collected
        //     collected = true;

        //     // Hide the object
        //     gameObject.SetActive(false);
        // }
    }
}
