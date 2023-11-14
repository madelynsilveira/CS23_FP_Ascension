using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerFly : MonoBehaviour
{
      //public Animator anim;
      public Rigidbody2D rb;
      private float flyForce = 8f;
      public int numFeathers = 0;
      private float maxFlyTime = 0f;
      public bool canFly = false;
      private bool isFlying = false;
      public float flyTimer;
      public GameObject timerCircle;
      
      public bool isAlive = true;
      private bool movingUp = false;
      private bool isColliding = false;
      private bool waitingToExit = false;

      void Start(){
            //anim = gameObject.GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();

            // temporary
            UpdateFlyingAbilities();
            flyTimer = 0f;
      }

      void Update() {
            if (!movingUp && rb.velocity.y > 0) {
                  movingUp = true;
                  gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            } else if (movingUp && rb.velocity.y <= 0) {
                  movingUp = false;
                  if (isColliding) {
                        waitingToExit = true;
                  } else {
                        gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
                  }
            }

           if ((Input.GetKeyDown("up") || Input.GetKeyDown("w")) && 
               (canFly) && (isAlive) && (flyTimer > 0f)) {
                  canFly = false;
                  Fly();
            }

            UpdateFlyingAbilities();
      }

      void FixedUpdate() {
            // update fly timer
            if (isFlying) {
                if (flyTimer > 0f) {
                    flyTimer -= Time.deltaTime;
                    if (flyTimer < 0f) {
                        flyTimer = 0f;
                    }
                }
            } else if (flyTimer < maxFlyTime) {
                flyTimer += Time.deltaTime;
                if (flyTimer > maxFlyTime) {
                  flyTimer = maxFlyTime;
                }
            }

            if (flyTimer == 0f) {
                  timerCircle.GetComponent<Image>().fillAmount = 0;
            } else {
                  timerCircle.GetComponent<Image>().fillAmount = flyTimer / maxFlyTime;
            }
      }

      void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.layer == 3 /* ground */) {
                isFlying = false;
            }
      }

      void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.layer == 3) {
                  isColliding = true;
            }
      }

      void OnTriggerExit2D(Collider2D other) {
            isColliding = false;
            if (waitingToExit) {
                  waitingToExit = false;
                  gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
            }
      }

      public void Fly() {
            //flyTimer += 1;
            isFlying = true;
            gameObject.GetComponent<AudioSource>().Play();
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
            gameObject.GetComponent<AudioSource>().Stop();
            canFly = true;
      }

      void UpdateFlyingAbilities() {
            if (numFeathers <= 0) {
                  canFly = false;
            } else {
                  canFly = true;
                  flyForce = 8f + 2f * (numFeathers / 5);
                  maxFlyTime = numFeathers * 1.2f;
            }
      }
}
