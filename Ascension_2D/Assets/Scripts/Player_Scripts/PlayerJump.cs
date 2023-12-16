using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

      //public Animator anim;
      public Rigidbody2D rb;
      private float jumpForce = 10f;
      public Transform feet;
      public LayerMask groundLayer;
      public LayerMask enemyLayer;
      public bool canJump = false;
      public int jumpTimes = 0;
      public bool isAlive = true;
      //public static bool jumpFrozen;
      //public AudioSource JumpSFX;

      // for a less floaty jump
      public float fallMultiplier = 2.5f;
      public float lowJumpMultiplier = 2f;

      void Start(){
            //anim = gameObject.GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
      }

      void Update() {
            // if ((IsGrounded()) || (jumpTimes <= 1)){
            //       canJump = true;
            // }  else if (jumpTimes > 1){
            //       canJump = false;
            // }

           if (((Input.GetKeyDown("up")) || (Input.GetKeyDown("w")) || (Input.GetKeyDown("space"))) && ((IsGrounded()) || (jumpTimes <= 1)) && (isAlive == true)/* && !jumpFrozen*/) {
                  Jump();
            }

            // make the jump less floaty
            if (rb.velocity.y < 0) {
                  rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            } else if (rb.velocity.y > 0 && !Input.GetButton ("Jump")){
                  rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
      }

      public void Jump() {
            jumpTimes += 1;
            rb.velocity = Vector2.up * jumpForce;
            // anim.SetTrigger("Jump");
            // JumpSFX.Play();

            //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
            //rb.velocity = movement;
      }

      public bool IsGrounded() {
            Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 1f, groundLayer);
            Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 1f, enemyLayer);
            if ((groundCheck != null) || (enemyCheck != null)) {
                  jumpTimes = 0;
                  return true;
            }
            return false;
      }
}