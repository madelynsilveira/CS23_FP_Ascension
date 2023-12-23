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
      private SpriteRenderer playersprite;
      private float originalScale;
      private float squashScale;
      private float stretchScale;
      private bool squashed = true;


      void Start(){
            rb = GetComponent<Rigidbody2D>();
            originalScale = transform.localScale.y;
            squashScale = originalScale * 0.8f;
            stretchScale = originalScale * 1.2f;;
            playersprite = GetComponent<SpriteRenderer>();
      }

      void Update() {

            grounded = IsGrounded();

            // falling
            if (rb.velocity.y < -0.5f) {
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
            StartCoroutine(stretch());
            gameObject.GetComponentInChildren<Animator>().SetTrigger("player_jump");
            StartCoroutine(Fall());
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
                  if (!squashed) {
                        StartCoroutine(squash());
                  }

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

      IEnumerator stretch() {
            // stretch the sprite
            transform.localScale = new Vector3(transform.localScale.x, stretchScale, transform.localScale.z);

            // Wait for a short duration
            yield return new WaitForSeconds(0.3f); 

            // Reset the player's scale
            transform.localScale = new Vector3(transform.localScale.x, originalScale, transform.localScale.z);
            squashed = false;
      }

      IEnumerator squash() {

            // stretch the sprite
            transform.localScale = new Vector3(transform.localScale.x, squashScale, transform.localScale.z);

            // Wait for a short duration
            yield return new WaitForSeconds(0.3f); 

            // Reset the player's scale
            transform.localScale = new Vector3(transform.localScale.x, originalScale, transform.localScale.z);
            squashed = true;
      }
}