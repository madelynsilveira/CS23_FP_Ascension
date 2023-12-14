using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    private Animator anim;
       //private GameHandler gameHandler;

       private Transform player;
       public Transform AttackPoint;
       private float attackRange = 2f;
       private float damageRange = 2f;
       //public LayerMask playerLayer;

       public float damage = 10;
       private float distanceToMouth;
       public float timeToNextAttack = 2f;
       public bool canAttack = true;


       void Start(){
              //gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler>();
              anim = GetComponentInChildren<Animator>();
              player = GameObject.FindWithTag("Player").transform;
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
              Debug.Log("Entering Attack");
              if (distanceToMouth < damageRange){
                Debug.Log("Attacking");
                StartCoroutine(HurtPlayer());
                //PlayerHeal.playerGetHit(damage);
              }
              else {
                gameObject.GetComponent<EnemyPatrol>().isAttacking=false;
                //anim.SetBool("Attack", false);
                //EnemyPatrol.isAttacking = false;
                Debug.Log("Not attacking anymore");
              }

              // Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, damageRange, playerLayer);
              // foreach(Collider2D player in hitPlayer){
                     // Debug.Log("We hit " + player.name);
                     // gameHandler.playerGetHit(damage);
              // }
       }

       IEnumerator AttackDelay(){
              canAttack = false;
              anim.SetBool("Walk", false);
              yield return new WaitForSeconds(1f);
              gameObject.GetComponent<EnemyPatrol>().isAttacking=false;
              yield return new WaitForSeconds(1f);
              Debug.Log("Waiting to attack");
              canAttack = true;
              //gameObject.GetComponent<EnemyPatrol>().isAttacking=false;
              //anim.SetBool("Attack", false);
              //Attack();
       }

       IEnumerator HurtPlayer() {
              yield return new WaitForSeconds(0.1f);
              PlayerHeal.playerGetHit(damage);
       }

       //NOTE: to help see the attack sphere in editor:
       void OnDrawGizmos(){
              Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
              Gizmos.DrawWireSphere(AttackPoint.position, damageRange);
       }
}
