using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHeal : MonoBehaviour
{
    public static bool canHeal;
    public static bool beingAttacked;
    private bool attackFinished;
    public GameObject npc;

    public float maxHealth = 100f;
    public float health;
    public GameObject life1;
    public GameObject healthBG;

    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
        UpdateHealth();
        beingAttacked = false;
        attackFinished = true;
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
            // healing effect

            GameHandler.lifeEnergyScore--;
            Image lifeEnergyBar = GameObject.FindWithTag("LifeEnergyBar").GetComponent<Image>();
            lifeEnergyBar.fillAmount = GameHandler.lifeEnergyScore / GameHandler.maxLifeEnergy;
            

            // if (SceneManager.GetActiveScene().name == "Tutorial") {
            //     Text instructionsText = GameObject.FindWithTag("Instructions").GetComponent<Text>();
            //     instructionsText.text = "Use the up arrow or [w] to flap your wings and fly. The circle in the top left is the flying timer which shows how long you can fly for and will refill while you are not flying.";
            // }
        }

        if (beingAttacked && attackFinished) {
            attackFinished = false;
            health -= 5f;
            UpdateHealth();
            if (health == 0) {
                SceneManager.LoadScene("LoseScene");
            }
            StartCoroutine(Attack());
        }
    }

    public void UpdateHealth() {
        life1.GetComponent<Image>().fillAmount = health / maxHealth;
        healthBG.GetComponent<Image>().fillAmount = (maxHealth - health) / maxHealth;
    }

    IEnumerator Attack() {
        yield return new WaitForSeconds(0.5f);
        attackFinished = true;
    }
}
