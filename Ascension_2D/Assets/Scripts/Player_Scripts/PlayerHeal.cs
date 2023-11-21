using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m") && GameHandler.lifeEnergyScore > 0) {
            // if full health
            PlayerFly.numFeathers++;
            // else heal player

            GameHandler.lifeEnergyScore--;
            Text lifeText = GameObject.FindWithTag("LifeEnergyText").GetComponent<Text>();
            lifeText.text = "" + GameHandler.lifeEnergyScore;
        } else if (Input.GetKeyDown("n") && GameHandler.lifeEnergyScore > 0) {
            // heal npc player
            Debug.Log("Healing enemy");

            GameHandler.lifeEnergyScore--;
            Text lifeText = GameObject.FindWithTag("LifeEnergyText").GetComponent<Text>();
            lifeText.text = "" + GameHandler.lifeEnergyScore;
        }
    }
}
