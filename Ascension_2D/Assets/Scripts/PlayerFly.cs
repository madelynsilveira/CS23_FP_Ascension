using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : MonoBehaviour
{
      //public Animator anim;
      public Rigidbody2D rb;
      public float flyForce = 10f;
      //public Transform feet;
      //public LayerMask groundLayer;
      //public LayerMask enemyLayer;
      public bool canFly = false;
      private bool isFlying = false;
      public float flyTimer = 0f;
      public float maxFlyTime = 5f;
      public bool isAlive = true;
      //public AudioSource FlySFX;

      void Start(){
            //anim = gameObject.GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();

            // temporary
            canFly = true;
      }

      void Update() {
            // update fly timer
            if (isFlying) {
                if (flyTimer > 0f) {
                    flyTimer -= Time.deltaTime;
                }
            } else if (flyTimer < maxFlyTime) {
                flyTimer += Time.deltaTime;
            }

           if ((Input.GetKeyDown("up")) && (canFly) && (isAlive) && (flyTimer > 0f)) {
                  canFly = false;
                  Fly();
            }
      }

      void OnCollisionEnter2D(Collision2D other) {
            Debug.Log("Collision Detected");
            Debug.Log("other layer: " + other.gameObject.layer);
            if (other.gameObject.layer == 3 /* ground */) {
                isFlying = false;
            }
      }

      public void Fly() {
            //flyTimer += 1;
            isFlying = true;
            rb.velocity = Vector2.up * flyForce;
            StartCoroutine(Flap());
            // wait for wing flap
            
            // anim.SetTrigger("Jump");
            // JumpSFX.Play();

            //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
            //rb.velocity = movement;
      }

      IEnumerator Flap() {
            yield return new WaitForSeconds(0.5f);
            canFly = true;
      }

    //   public bool IsGrounded() {
    //         Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 2f, groundLayer);
    //         Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 2f, enemyLayer);
    //         if ((groundCheck != null) || (enemyCheck != null)) {
    //               //Debug.Log("I am trouching ground!");
    //               jumpTimes = 0;
    //               return true;
    //         }
    //         return false;
    //   }
}
