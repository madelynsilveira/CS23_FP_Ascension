using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    private Animator anim;
       //private GameHandler gameHandler;

       private Transform player;
       public Transform AttackPoint;
       private float attackRange = 3f;
       private float damageRange = 5f;
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
              Debug.Log("I am biting the player!");
              if (distanceToMouth < damageRange){
                gameObject.GetComponent<EnemyPatrol>().isAttacking=true;
                //PlayerHeal.beingAttacked = true;
                PlayerHeal.playerGetHit(damage);
              }
              else {
                gameObject.GetComponent<EnemyPatrol>().isAttacking=false;
              }

              // Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, damageRange, playerLayer);
              // foreach(Collider2D player in hitPlayer){
                     // Debug.Log("We hit " + player.name);
                     // gameHandler.playerGetHit(damage);
              // }
       }

       IEnumerator AttackDelay(){
              canAttack = false;
              yield return new WaitForSeconds(timeToNextAttack);
              canAttack = true;
       }

       //NOTE: to help see the attack sphere in editor:
       void OnDrawGizmos(){
              Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
              Gizmos.DrawWireSphere(AttackPoint.position, damageRange);
       }
}
