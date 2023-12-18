using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class PlayerMove : MonoBehaviour {

      public Animator anim;
      private SpriteRenderer playerSprite;
      public Rigidbody2D rb2D;

      private bool FaceRight = true; // determine which way player is facing.
      public static float runSpeed = 10f;
      public float startSpeed = 10f;
      //public float freezeDistance = 25f;
      public static bool isFrozen;
      private bool inLava = false;
      public static bool keyFound = false;
      //public AudioSource WalkSFX;
      private Vector3 hMove;
      // public int lifeEnergyScore = 0;
      //public GameObject lifeEnergyObj;

      void Start(){
           anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
           isFrozen = false;

            // Get the SpriteRenderer component attached to the GameObject
            playerSprite = GetComponentInChildren<SpriteRenderer>();

            // Check if a SpriteRenderer component is found
            if (playerSprite != null)
            {
                  // Change the color to red (you can use any color you want)
                  playerSprite.color = new Color(0.0f, 0.0f, 0.0f);
            }
            else
            {
                  // Log a warning if no SpriteRenderer component is found
                  Debug.LogWarning("SpriteRenderer component not found on this GameObject.");
            }
      }

      void Update(){
            playerSprite.color = new Color(0.0f, 0.0f, 0.0f);
            //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
            hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            
            // set movement animation
            if ((hMove.x > 0.5) || (hMove.x < -0.5)) {
                  anim.SetBool("player_walk", true);
            } else {
                  anim.SetBool("player_walk", false);
            }

            // adjust falling gravity
            if (rb2D.velocity.y < 0) {
                  if (rb2D != null)
                  {
                        // Adjust the gravity scale
                        rb2D.gravityScale = 2;
                  }
            } else {
                  rb2D.gravityScale = 1;
            }


            if (PlayerHeal.isAlive){
                  /*if ((transform.position + hMove * runSpeed * Time.deltaTime).x > -freezeDistance &&
                      (transform.position + hMove * runSpeed * Time.deltaTime).x < freezeDistance) {*/
                        transform.position = transform.position + hMove * runSpeed * Time.deltaTime;
                  //}
                  

                  // if (Input.GetAxis("Horizontal") != 0){
                  //       animator.SetBool ("Walk", true);
                  //       if (!WalkSFX.isPlaying){
                  //             WalkSFX.Play();
                  //      }
                  // } else {
                  //      animator.SetBool ("Walk", false);
                  //      WalkSFX.Stop();
                  // }

                  // Turning: Reverse if input is moving the Player right and Player faces left
                 if ((hMove.x <0 && FaceRight) || (hMove.x >0 && !FaceRight)){
                        playerTurn();
                  }
            }
      }

      void FixedUpdate(){
            //slow down on hills / stops sliding from velocity
            if (hMove.x == 0){
                  rb2D.velocity = new Vector2(rb2D.velocity.x / 1.1f, rb2D.velocity.y) ;
            }
      }

      private void playerTurn(){
            // NOTE: Switch player facing label
            FaceRight = !FaceRight;

            // NOTE: Multiply player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
      }

      void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Key") {
                  keyFound = true;
                  Destroy(GameObject.FindWithTag("Key"));
            } else if (other.gameObject.tag == "Portal" && keyFound) {
                  if (SceneManager.GetActiveScene().name == "Level1") {
                        GameHandler.level1Complete = true;
                  } else if (SceneManager.GetActiveScene().name == "Level2") {
                        GameHandler.level2Complete = true;
                  } else if (SceneManager.GetActiveScene().name == "Level3") {
                        GameHandler.level3Complete = true;
                  } else if (SceneManager.GetActiveScene().name == "Level4") {
                        GameHandler.level4Complete = true;
                  } else if (SceneManager.GetActiveScene().name == "Level5") {
                        GameHandler.level5Complete = true;
                  } else if (SceneManager.GetActiveScene().name == "Level6") {
                        GameHandler.level6Complete = true;
                  }
                  
                  if (SceneManager.GetActiveScene().name == "Level7") {
                        SceneManager.LoadScene("winScene");
                  } else {
                        SceneManager.LoadScene("MainMenu");
                  }
            } else if (other.gameObject.tag == "Boundary") {
                  transform.position = new Vector2 (-72f, -1f);
            } else if (other.gameObject.tag == "Lava") {
                  inLava = true;
                  StartCoroutine(HurtByLava());
            }
      }

      void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.tag == "Lava") {
                  inLava = false;
            }
      }

      IEnumerator HurtByLava() {
            if (inLava) {
                  gameObject.GetComponentInChildren<Animator>().SetTrigger("player_getHurt");
                  PlayerHeal.health -= 5f;
                  PlayerHeal.UpdateHealth();
                  yield return new WaitForSeconds(0.5f);
                  StartCoroutine(HurtByLava());
            }
      }
}