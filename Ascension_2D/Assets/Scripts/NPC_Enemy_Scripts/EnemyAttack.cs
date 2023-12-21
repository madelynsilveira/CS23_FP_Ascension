using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour {
    private Animator anim;
       //private GameHandler gameHandler;

       private Transform player;
       public Transform AttackPoint;
       private float attackRange = 2.5f;
       private float damageRange = 2.5f;
       //public LayerMask playerLayer;

       public float damage = 10;
       private float distanceToMouth;
       public float timeToNextAttack = 2f;
       public bool canAttack;
       public bool isAlive;


       void Start(){
              //gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler>();
              anim = GetComponentInChildren<Animator>();
              player = GameObject.FindWithTag("Player").transform;
              if (SceneManager.GetActiveScene().name == "Tutorial") {
                     canAttack = false;
              } else {
                     canAttack = true;
              }
              isAlive = true;
       }

       void Update(){
              distanceToMouth = Vector3.Distance(player.position, AttackPoint.position);
              
              if ((distanceToMouth < attackRange) && (canAttack)){
                     Attack();
                     StartCoroutine(AttackDelay());
              }
       }

       void Attack(){
              anim.SetTrigger("Attack");
              gameObject.GetComponent<EnemyPatrol>().isAttacking=true;
              if (distanceToMouth < damageRange){
                StartCoroutine(HurtPlayer());
                //PlayerHeal.playerGetHit(damage);
              }
              else {
                gameObject.GetComponent<EnemyPatrol>().isAttacking=false;
              }

              // Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, damageRange, playerLayer);
              // foreach(Collider2D player in hitPlayer){
                     // gameHandler.playerGetHit(damage);
              // }
       }

       IEnumerator AttackDelay(){
              canAttack = false;
              anim.SetBool("Walk", false);
              yield return new WaitForSeconds(1.5f);
              gameObject.GetComponent<EnemyPatrol>().isAttacking=false;
              canAttack = true;
       }

       IEnumerator HurtPlayer() {
              yield return new WaitForSeconds(0.1f);
              if (isAlive) {
                     PlayerMove.redPlayerArt.SetActive(true);
                     PlayerMove.playerArt.SetActive(false);
                     PlayerHeal.isAlive = false;
                     PlayerHeal.playerGetHit(damage);
                     yield return new WaitForSeconds(0.4f);
                     PlayerHeal.isAlive = true;
                     PlayerMove.playerArt.SetActive(true);
                     PlayerMove.redPlayerArt.SetActive(false);
              }
       }

       //NOTE: to help see the attack sphere in editor:
       void OnDrawGizmos(){
              Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
              Gizmos.DrawWireSphere(AttackPoint.position, damageRange);
       }
}
