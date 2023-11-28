using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class PlayerMove : MonoBehaviour {

      public Animator anim;
      public Rigidbody2D rb2D;
      private bool FaceRight = true; // determine which way player is facing.
      public static float runSpeed = 10f;
      public float startSpeed = 10f;
      //public float freezeDistance = 25f;
      public static bool isFrozen;
      //public AudioSource WalkSFX;
      private Vector3 hMove;
      // public int lifeEnergyScore = 0;
      //public GameObject lifeEnergyObj;

      void Start(){
           anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
           isFrozen = false;
      }

      void Update(){
            //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
            hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            
            if ((hMove.x > 0.5) || (hMove.x < -0.5)) {
                  anim.SetBool("player_walk", true);
            } else {
                  anim.SetBool("player_walk", false);
            }
            if (!isFrozen){
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

      // void OnTriggerEnter2D(Collider2D other) {
      //       Debug.Log("Other object tag: " + other.gameObject.tag);
      //       if (other.gameObject.tag == "Food") {
      //             Destroy(other.gameObject);
      //             lifeEnergyScore += 1;
      //             Text lifeEnergyText = lifeEnergyObj.GetComponent<Text>();
      //             lifeEnergyText.text = "" + lifeEnergyScore;
      //       }
      // }

      private void playerTurn(){
            // NOTE: Switch player facing label
            FaceRight = !FaceRight;

            // NOTE: Multiply player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
      }

      void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Portal") {
                  if (SceneManager.GetActiveScene().name == "Level1") {
                        SceneManager.LoadScene("winScene");
                  } else {
                        GameHandler.tutorialComplete = true;
                        SceneManager.LoadScene("Level1");
                  }
            }
      }
}