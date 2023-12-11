using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHeal : MonoBehaviour
{
    //public static bool canHeal;
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
        if ((Input.GetKeyDown("left shift") || Input.GetKeyDown("right shift")) && GameHandler.lifeEnergyScore > 0) {
            // adjust player life energy
            GameHandler.lifeEnergyScore--;
            Image lifeEnergyBar = GameObject.FindWithTag("LifeEnergyBar").GetComponent<Image>();
            lifeEnergyBar.fillAmount = GameHandler.lifeEnergyScore / GameHandler.maxLifeEnergy;
            Debug.Log(GameHandler.lifeEnergyScore);
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
