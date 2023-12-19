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
    public static GameObject player;
    public static bool isAlive;
    private GameObject[] enemyArray;
    private float[] enemyDistanceArray;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial") {
            health = 90f;
        } else {
            health = 100f;
        }
        isAlive = true;
        healthBar = GameObject.FindWithTag("HealthBar");
        healthBarBG = GameObject.FindWithTag("HealthBarBG");
        player = GameObject.FindWithTag("Player");
        UpdateHealth();
        // beingAttacked = false;
        // attackFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f) {
            StartCoroutine(EndLevel());
        }

        if ((Input.GetKeyDown("left shift") || Input.GetKeyDown("right shift")) && GameHandler.lifeEnergyScore > 0 && isAlive) {
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
                    enemyArray[closestEnemy].GetComponentInChildren<ParticleSystem>().Play();
                    StartCoroutine(HealEnemy(enemyArray[closestEnemy]));
                    GameHandler.soulsHealed++;

                    player.GetComponentInChildren<Animator>().SetTrigger("player_healEnemy");

                    // adjust player life energy
                    GameHandler.lifeEnergyScore--;
                    Image lifeEnergyBar = GameObject.FindWithTag("LifeEnergyBar").GetComponent<Image>();
                    lifeEnergyBar.fillAmount = GameHandler.lifeEnergyScore / GameHandler.maxLifeEnergy;
                }
            }
        }

        if ((Input.GetKeyDown("down") || Input.GetKeyDown("s")) && GameHandler.lifeEnergyScore > 0 && isAlive) {
            if (health < maxHealth) {
                health += 10f;
                if (health > maxHealth) {
                    health = maxHealth;
                }

                player.GetComponentInChildren<Animator>().SetTrigger("player_healSelf");

                // adjust player life energy
                GameHandler.lifeEnergyScore--;
                Image lifeEnergyBar = GameObject.FindWithTag("LifeEnergyBar").GetComponent<Image>();
                lifeEnergyBar.fillAmount = GameHandler.lifeEnergyScore / GameHandler.maxLifeEnergy;
            }
            UpdateHealth();

            
        }
    }

    public static void UpdateHealth() {
        healthBar.GetComponent<Image>().fillAmount = health / maxHealth;
        healthBarBG.GetComponent<Image>().fillAmount = (maxHealth - health) / maxHealth;
    }

    public static void playerGetHit(float damage) {
        health -= damage;
        
        if (health <= 0f) {
            isAlive = false;
            player.GetComponentInChildren<Animator>().SetTrigger("player_die");
        } else {
            player.GetComponentInChildren<Animator>().SetTrigger("player_getHurt");
        }
        UpdateHealth();
    }

    IEnumerator HealEnemy(GameObject enemy) {
        yield return new WaitForSeconds(0.5f);
        enemy.SetActive(false);
        yield return new WaitForSeconds(1f);
        Destroy(enemy);
    }

    IEnumerator EndLevel() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("LoseScene");
    }
}
