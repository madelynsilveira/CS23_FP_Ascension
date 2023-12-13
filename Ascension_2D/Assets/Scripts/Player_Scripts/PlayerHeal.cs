using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHeal : MonoBehaviour
{
    //public static bool canHeal;
    public static bool beingAttacked;
    //private bool attackFinished;
    public GameObject npc;

    public static float maxHealth = 100f;
    public static float health;
    public static GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
        healthBar = GameObject.FindWithTag("HealthBar");
        UpdateHealth();
        // beingAttacked = false;
        // attackFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("left shift") || Input.GetKeyDown("right shift")) && GameHandler.lifeEnergyScore > 0) {
            // adjust player life energy
            GameHandler.lifeEnergyScore--;
            Image lifeEnergyBar = GameObject.FindWithTag("LifeEnergyBar").GetComponent<Image>();
            lifeEnergyBar.fillAmount = GameHandler.lifeEnergyScore / GameHandler.maxLifeEnergy;
            Debug.Log(GameHandler.lifeEnergyScore);
        }

        //Debug.Log("beingAttacked: " + beingAttacked);
        //Debug.Log("attackFinished: " + attackFinished);

        // if (beingAttacked && attackFinished) {
        //     attackFinished = false;
        //     health -= 5f;
        //     UpdateHealth();
        //     if (health == 0) {
        //         SceneManager.LoadScene("LoseScene");
        //     }
        //     StartCoroutine(Attack());
        // }
    }

    public static void UpdateHealth() {
        Debug.Log("in update health");
        healthBar.GetComponent<Image>().fillAmount = health / maxHealth;
        // healthBG.GetComponent<Image>().fillAmount = (maxHealth - health) / maxHealth;
    }

    public static void playerGetHit(float damage) {
        health -= damage;
        UpdateHealth();
        if (health == 0) {
            SceneManager.LoadScene("LoseScene");
        }
    }

    // IEnumerator Attack() {
    //     // if (attackFinished) {
    //     //     attackFinished = false;
    //     //     health -= 5f;
    //     //     UpdateHealth();
    //     //     if (health == 0) {
    //     //         SceneManager.LoadScene("LoseScene");
    //     //     }

    //     yield return new WaitForSeconds(0.5f);
    //     attackFinished = true;
    //     // }
    // }
}
