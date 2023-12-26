using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHeal : MonoBehaviour
{
    public static bool beingAttacked;
    public GameObject npc;

    public static float maxHealth = 100f;
    public static float health;
    public static GameObject healthBar;
    public static GameObject healthBarBG;
    public static GameObject player;
    public static bool isAlive;
    private bool canHealEnemy;
    private int numDemons;
    private Text DemonText;
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
        DemonText = GameObject.FindWithTag("DemonsText").GetComponent<Text>();
        enemyArray = GameObject.FindGameObjectsWithTag("NPC");
        numDemons = enemyArray.Length;
        UpdateDemonCount();
        player = GameObject.FindWithTag("Player");
        UpdateHealth();
        canHealEnemy = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f) {
            StartCoroutine(EndLevel());
        }

        if ((Input.GetKeyDown("left shift") || Input.GetKeyDown("right shift") || Input.GetKeyDown("e")) && GameHandler.lifeEnergyScore > 0 && isAlive && canHealEnemy) {
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

    private void UpdateDemonCount() {
        DemonText.text = numDemons.ToString();
        // if (numDemons == 1) {
        //     DemonText.text = "1 remaining demon";
        // } else {
        //     DemonText.text = numDemons + " remaining demons";
        // }
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
        canHealEnemy = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(enemy);
        numDemons--;
        UpdateDemonCount();
        PlayerMove.playerArt.SetActive(true);
        PlayerMove.redPlayerArt.SetActive(false);
        if (health > 0f) {
            isAlive = true;
        }
        canHealEnemy = true;
    }

    IEnumerator EndLevel() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("LoseScene");
    }

    // public IEnumerator squash() {
    //     float originalScale = transform.localScale.y;

    //     // stretch the sprite
    //     transform.localScale = new Vector3(transform.localScale.x, transform.localScale * 1.1, transform.localScale.z);

    //     // Wait for a short duration
    //     yield return new WaitForSeconds(0.1f); 

    //             // stretch the sprite
    //     transform.localScale = new Vector3(transform.localScale.x, transform.localScale * , transform.localScale.z);
    // }
}
