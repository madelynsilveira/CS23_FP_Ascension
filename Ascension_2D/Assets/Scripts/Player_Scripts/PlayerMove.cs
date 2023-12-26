using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class PlayerMove : MonoBehaviour {

      public Animator anim;
      public Rigidbody2D rb2D;
      private Vector3 hMove;

      public static float runSpeed = 10f;
      public static bool isFrozen;
      public static bool keyFound;  // to open gate
      public bool FaceRight = true; // which way player is facing
      private bool inLava;          // get hurt by lava
      
      // changing visual effects
      public static GameObject redPlayerArt;
      public static GameObject playerArt;
      public static GameObject keyNotFoundArt;
      public static GameObject keyFoundArt;

      void Start(){

            // pulling compunents
            anim = gameObject.GetComponentInChildren<Animator>();
            rb2D = transform.GetComponent<Rigidbody2D>();

            // setting variables
            isFrozen = false;
            inLava = false;
            keyFound = false;

            // for player get hurt color change
            playerArt = GameObject.FindWithTag("PlayerArt");
            redPlayerArt = GameObject.FindWithTag("PlayerArtRed");
            playerArt.SetActive(true);
            redPlayerArt.SetActive(false);

            // for top menu key indication
            keyNotFoundArt = GameObject.FindWithTag("KeyNotFound");
            keyFoundArt = GameObject.FindWithTag("KeyFound");
            keyNotFoundArt.SetActive(true);
            keyFoundArt.SetActive(false);
      }

      void Update(){

            // Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
            hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);

            // adjust falling gravity
            if (rb2D.velocity.y < 0) {
                  if (rb2D != null) {
                        // Adjust the gravity scale
                        rb2D.gravityScale = 2;
                  }
            } else {
                  rb2D.gravityScale = 1;
            }


            if (PlayerHeal.isAlive){
                  transform.position = transform.position + hMove * runSpeed * Time.deltaTime;

                  // set movement animation
                  if ((hMove.x > 0.5) || (hMove.x < -0.5)) {
                        anim.SetBool("player_walk", true);
                  } else {
                        anim.SetBool("player_walk", false);
                  }

                  // Turning: Reverse if input is moving the Player right and Player faces left
                 if ((hMove.x <0 && FaceRight) || (hMove.x >0 && !FaceRight)){
                        playerTurn();
                  }
            }

            // update key marker in top menu
            if (keyFound) {
                  keyNotFoundArt.SetActive(false);
                  keyFoundArt.SetActive(true);
            }
      }

      void FixedUpdate() {

            // slow down on hills / stops sliding from velocity
            if (hMove.x == 0){
                  rb2D.velocity = new Vector2(rb2D.velocity.x / 1.1f, rb2D.velocity.y) ;
            }
      }

      private void playerTurn() {

            // Switch player facing label
            FaceRight = !FaceRight;

            // Multiply player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
      }

      void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Key") {
                  keyFound = true;
                  Destroy(GameObject.FindWithTag("Key"));
            } else if (other.gameObject.tag == "Portal" && keyFound) {
                  GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("NPC");;
                  if (SceneManager.GetActiveScene().name == "Tutorial" && enemyArray.Length == 0) {
                        // GameHandler.tutorialStarred = true;
                  } else if (SceneManager.GetActiveScene().name == "Level1") {
                        GameHandler.level1Complete = true;
                        if (enemyArray.Length == 0) {
                              GameHandler.level1Starred = true;
                        }
                  } else if (SceneManager.GetActiveScene().name == "Level2") {
                        GameHandler.level2Complete = true;
                        if (enemyArray.Length == 0) {
                              GameHandler.level2Starred = true;
                        }
                  } else if (SceneManager.GetActiveScene().name == "Level3") {
                        GameHandler.level3Complete = true;
                        if (enemyArray.Length == 0) {
                              GameHandler.level3Starred = true;
                        }
                  } else if (SceneManager.GetActiveScene().name == "Level4") {
                        GameHandler.level4Complete = true;
                        if (enemyArray.Length == 0) {
                              GameHandler.level4Starred = true;
                        }
                  } else if (SceneManager.GetActiveScene().name == "Level5") {
                        GameHandler.level5Complete = true;
                        if (enemyArray.Length == 0) {
                              GameHandler.level5Starred = true;
                        }
                  } else if (SceneManager.GetActiveScene().name == "Level6" && enemyArray.Length == 0) {
                        GameHandler.level6Starred = true;
                  }
                  
                  StartCoroutine(OpenGates());
            } else if (other.gameObject.tag == "Boundary") {
                  transform.position = new Vector2 (-72f, -1f);
            } else if (other.gameObject.tag == "Lava" && PlayerHeal.isAlive) {
                  inLava = true;
                  StartCoroutine(HurtByLava());
            } else if (other.gameObject.tag == "Checkpoint") {
                  Tutorial_Bottom_Script.DisplayText(Tutorial_Bottom_Script.messages[Tutorial_Bottom_Script.currIndex]);
                  Tutorial_Bottom_Script.currIndex++;
                  Destroy(other.gameObject);
            }
      }

      void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.tag == "Lava") {
                  inLava = false;
            }
      }

      IEnumerator HurtByLava() {
            if (inLava) {
                  PlayerHeal.health -= 5f;
                  PlayerHeal.UpdateHealth();
                  redPlayerArt.SetActive(true);
                  playerArt.SetActive(false);

                  if (PlayerHeal.health <= 0f) {
                        PlayerHeal.isAlive = false;
                        gameObject.GetComponentInChildren<Animator>().SetTrigger("player_die");
                  } else {
                        yield return new WaitForSeconds(0.25f);
                        playerArt.SetActive(true);
                        redPlayerArt.SetActive(false);
                        yield return new WaitForSeconds(0.25f);
                        StartCoroutine(HurtByLava());
                  }  
            }
      }

      IEnumerator OpenGates() {
            PlayerHeal.isAlive = false;
            gameObject.GetComponentInChildren<Animator>().SetBool("player_walk", false);
            gameObject.GetComponentInChildren<Animator>().SetTrigger("player_idle");
            GameObject.FindWithTag("Portal").GetComponentInChildren<Animator>().SetTrigger("openGate");
            yield return new WaitForSeconds(1f);
            if (SceneManager.GetActiveScene().name == "Level6") {
                  SceneManager.LoadScene("winScene");
            } else {
                  SceneManager.LoadScene("MainMenu");
            }
      }
}