using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

      //public Animator anim;
      public Rigidbody2D rb;
      private float jumpForce = 12f;
      public Transform feet;
      public LayerMask groundLayer;
      public LayerMask enemyLayer;
      public bool canJump = false;
      public int jumpTimes = 0;
      private bool grounded;
      //public static bool jumpFrozen;
      //public AudioSource JumpSFX;

      // for a less floaty jump
      public float fallMultiplier = 2.5f;
      public float lowJumpMultiplier = 2f;

      // Stretch and Squash parameters
      // private SpriteRenderer playersprite;
      // private Vector3 originalScale;
      // private Vector3 squashScale;
      // private Vector3 stretchScale;
      // private bool squashed = true;


      void Start(){
            rb = GetComponent<Rigidbody2D>();
            // float xNoSign = Mathf.Abs(transform.localScale.x);
            // originalScale = new Vector3(xNoSign, transform.localScale.y, transform.localScale.z);
            // squashScale = new Vector3(xNoSign * 1.2f, originalScale.y * 0.8f, originalScale.z);
            // stretchScale = new Vector3(xNoSign * 0.8f, originalScale.y * 1.2f, originalScale.z);
            // playersprite = GetComponent<SpriteRenderer>();
      }

      void Update() {
            // Debug.Log(playerMove.FaceRight);

            grounded = IsGrounded();

            // falling
            if (rb.velocity.y < -0.5f) {
                  // squashed = false;
                  gameObject.GetComponentInChildren<Animator>().SetBool("player_fall", true);
            } else if (grounded) {
                  gameObject.GetComponentInChildren<Animator>().SetBool("player_fall", false);
            }

            // player sprite animaiton
            if (((Input.GetKeyDown("up")) || (Input.GetKeyDown("w")) || (Input.GetKeyDown("space"))) && ((grounded) || (jumpTimes <= 1)) && (PlayerHeal.isAlive)) {
                  Jump();
            }

            // make the jump less floaty
            if (rb.velocity.y < 0) {
                  rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            } else if (rb.velocity.y > 0 && !((Input.GetKeyDown("up")) || (Input.GetKeyDown("w")))){
                  rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
      }

      public void Jump() {
            // StartCoroutine(stretch());
            gameObject.GetComponentInChildren<Animator>().SetTrigger("player_jump");
            // StartCoroutine(Fall());
            jumpTimes += 1;
            rb.velocity = Vector2.up * jumpForce;
      }

      public bool IsGrounded() {
            Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 1f, groundLayer);
            Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 1f, enemyLayer);
            
            //grounded
            if ((groundCheck != null) || (enemyCheck != null)) {
                  jumpTimes = 0;

                  // squash effect
                  // if (!squashed && (Mathf.Abs(rb.velocity.y) <= 0.2f)) {
                  //       StartCoroutine(squash());
                  // }

                  // transform parent for moving platforms
                  if (groundCheck.tag == "MovingPlatform") {
                        transform.parent = groundCheck.transform;
                  } else {
                        transform.parent = null;
                  }
                  return true;

            // not grounded
            } else {
                  transform.parent = null;
                  return false;
            }
            
      }

      IEnumerator Fall() {
            yield return new WaitForSeconds(0.25f);
            gameObject.GetComponentInChildren<Animator>().SetBool("player_fall", true);
      }

      // IEnumerator stretch() {

      //       // face correct direction
      //       float sign = (float)Mathf.Sign(rb.velocity.x);

      //       // stretch the sprite
      //       transform.localScale = new Vector3(stretchScale.x * sign, stretchScale.y, stretchScale.z);

      //       // Wait for a short duration
      //       yield return new WaitForSeconds(0.3f); 

      //       // Reset the player's scale
      //       transform.localScale = new Vector3(originalScale.x * sign, originalScale.y, originalScale.z);
      //       squashed = false;
      // }

      // IEnumerator squash() {
      //       // face correct direction
      //       float sign = (float)Mathf.Sign(rb.velocity.x);

      //       // squash the sprite
      //       transform.localScale = new Vector3(squashScale.x * sign, squashScale.y, squashScale.z);

      //       // Wait for a short duration
      //       yield return new WaitForSeconds(0.3f); 

      //       // Reset the player's scale
      //       transform.localScale = new Vector3(originalScale.x * sign, originalScale.y, originalScale.z);
      //       squashed = true;
      // }
}