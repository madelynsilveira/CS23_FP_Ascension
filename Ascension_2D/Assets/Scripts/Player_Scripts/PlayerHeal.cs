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
    public static GameObject healthBarBG;
    private GameObject[] enemyArray;
    private float[] enemyDistanceArray;

    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
        healthBar = GameObject.FindWithTag("HealthBar");
        healthBarBG = GameObject.FindWithTag("HealthBarBG");
        UpdateHealth();
        // beingAttacked = false;
        // attackFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("left shift") || Input.GetKeyDown("right shift")) && GameHandler.lifeEnergyScore > 0) {
            enemyArray = GameObject.FindGameObjectsWithTag("NPC");

            if (enemyArray.Length > 0) {
                // find closest enemy
                int closestEnemy = -1;
                float currMin = 5f;
                for (int i = 0; i < enemyArray.Length; i++) {
                    Transform childArt = enemyArray[i].transform.GetChild(0).transform;
                    if (Vector3.Distance (childArt.position, gameObject.transform.position) < currMin) {
                        closestEnemy = i;
                    }
                }
                
                // heal closest enemy if they exist
                if (closestEnemy != -1) {
                    enemyArray[closestEnemy].GetComponentInChildren<Animator>().SetTrigger("getHurt");
                    StartCoroutine(HealEnemy(enemyArray[closestEnemy]));
                    GameHandler.soulsHealed++;
                    Debug.Log("souls healed: " + GameHandler.soulsHealed);
                } else if (health < maxHealth) {
                    health += 5f;
                    UpdateHealth();
                }
            } else {
                health += 5f;
                UpdateHealth();
            }

            // adjust player life energy
            GameHandler.lifeEnergyScore--;
            Image lifeEnergyBar = GameObject.FindWithTag("LifeEnergyBar").GetComponent<Image>();
            lifeEnergyBar.fillAmount = GameHandler.lifeEnergyScore / GameHandler.maxLifeEnergy;
        }
    }

    public static void UpdateHealth() {
        healthBar.GetComponent<Image>().fillAmount = health / maxHealth;
        healthBarBG.GetComponent<Image>().fillAmount = (maxHealth - health) / maxHealth;
    }

    public static void playerGetHit(float damage) {
        health -= damage;
        UpdateHealth();
        if (health == 0) {
            SceneManager.LoadScene("LoseScene");
        }
    }

    IEnumerator HealEnemy(GameObject enemy) {
        yield return new WaitForSeconds(0.5f);
        Destroy(enemy);
    }
}
