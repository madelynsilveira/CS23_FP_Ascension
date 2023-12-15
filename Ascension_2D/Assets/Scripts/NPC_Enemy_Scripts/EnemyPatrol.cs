using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
       private Animator anim;
       private float speed = 6f;
       private float waitTime;
       public float startWaitTime = 2f;

       public Transform[] moveSpots;
       public int startSpot = 0;
       public bool moveForward = true;

       // Turning
       private int nextSpot;
       private int previousSpot;
       private Transform player;
       public bool faceRight = false;


       public bool isAttacking = false;
       private bool pursuing = false;

       void Start(){
              waitTime = startWaitTime;
              nextSpot = startSpot;
              anim = gameObject.GetComponentInChildren<Animator>();
              player = GameObject.FindWithTag("Player").transform;
       }

       void Update(){

            if (!isAttacking) {
              if (Vector2.Distance(transform.position, player.position) < 5f) {
                     Vector2 targetPos = new Vector2 (player.position.x, transform.position.y);
                     transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * 1.25f * Time.deltaTime);
                     anim.SetBool("Walk", true);
                     pursuing = true;
              } else if (Vector2.Distance(transform.position, moveSpots[nextSpot].position) > 0.5f) {
                     transform.position = Vector2.MoveTowards(transform.position, moveSpots[nextSpot].position, speed * Time.deltaTime);
                     anim.SetBool("Walk", true);
                     pursuing = false;
              } else if (Vector2.Distance(transform.position, moveSpots[nextSpot].position) <= 0.5f){
                     pursuing = false;
                     if (waitTime <= 0){
                            anim.SetBool("Walk", true);
                            if (moveForward == true){ previousSpot = nextSpot; nextSpot += 1; }
                            else if (moveForward == false){ previousSpot = nextSpot; nextSpot -= 1; }
                            waitTime = startWaitTime;
                     } else {
                            waitTime -= Time.deltaTime;
                            anim.SetBool("Walk", false);
                     }
              }

              //switch movement direction
              if (nextSpot == 0) {moveForward = true; }
              else if (nextSpot == (moveSpots.Length -1)) { moveForward = false; }

              //turning the enemy
              if (previousSpot < 0){ previousSpot = moveSpots.Length -1; }
              else if (previousSpot > moveSpots.Length -1){ previousSpot = 0; }

              if ((((!pursuing) && (previousSpot == 0)) || (pursuing && (player.position.x > transform.position.x))) && (faceRight)){ NPCTurn(); }
              else if ((((!pursuing) && (previousSpot == (moveSpots.Length -1))) || (pursuing && (player.position.x < transform.position.x))) && (!faceRight)) { NPCTurn(); }
              // NOTE1: If faceRight does not change, try reversing !faceRight, above
              // NOTE2: If NPC faces the wrong direction as it moves, set the sprite Scale X = -1.
            }
       }

       private void NPCTurn(){
              // NOTE: Switch player facing label (avoids constant turning)
              faceRight = !faceRight;

              // NOTE: Multiply player's x local scale by -1.
              Vector3 theScale = transform.localScale;
              theScale.x *= -1;
              transform.localScale = theScale;
       }

}