using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHeal : MonoBehaviour
{
    public static bool canHeal;
    public GameObject npc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown("m") && GameHandler.lifeEnergyScore > 0 && canHealSelf) {
        //     // if full health
        //     PlayerFly.numFeathers++;
        //     // else heal player

        //     GameHandler.lifeEnergyScore--;
        //     Text lifeText = GameObject.FindWithTag("LifeEnergyText").GetComponent<Text>();
        //     lifeText.text = "" + GameHandler.lifeEnergyScore;
            
        //     // if (SceneManager.GetActiveScene().name == "Tutorial") {
        //     //     Text instructionsText = GameObject.FindWithTag("Instructions").GetComponent<Text>();
        //     //     if (GameHandler.lifeEnergyScore == 2) {
        //     //         instructionsText.text = "Now you are at full health! Hit [m] again to grow bigger.";
        //     //     } else if (GameHandler.lifeEnergyScore == 1) {
        //     //         instructionsText.text = "Watch out! An enemy character has appeared! Hold down the down arrow or [s] in order to hide from them.";
        //     //         PlayerHide.canHide = true;
        //     //         canHealEnemy = true;
        //     //         canHealSelf = false;
        //     //         npc.SetActive(true);
        //     //     }
        //     // }
        // } else 
        if (Input.GetKeyDown("space") && GameHandler.lifeEnergyScore > 0 && canHeal) {
            // heal npc player
            Debug.Log("Healing enemy");

            GameHandler.lifeEnergyScore--;
            Text lifeText = GameObject.FindWithTag("LifeEnergyText").GetComponent<Text>();
            lifeText.text = "" + GameHandler.lifeEnergyScore;

            // if (SceneManager.GetActiveScene().name == "Tutorial") {
            //     Text instructionsText = GameObject.FindWithTag("Instructions").GetComponent<Text>();
            //     instructionsText.text = "Use the up arrow or [w] to flap your wings and fly. The circle in the top left is the flying timer which shows how long you can fly for and will refill while you are not flying.";
            // }
        }
    }
}
