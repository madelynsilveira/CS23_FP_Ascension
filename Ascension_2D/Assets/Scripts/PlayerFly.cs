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
      //public int flyTimer = 0;
      public bool isAlive = true;
      //public AudioSource FlySFX;

      void Start(){
            //anim = gameObject.GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();

            // temporary
            canFly = true;
      }

      void Update() {
            // replace with flying ability check
            // if ((IsGrounded()) || (jumpTimes <= 1)){
            //       canJump = true;
            // }  else if (jumpTimes > 1){
            //       canJump = false;
            // }

           if ((Input.GetKeyDown("up")) && (canFly) && (isAlive == true)) {
                  canFly = false;
                  Fly();
            }
      }

      public void Fly() {
            //flyTimer += 1;
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
