// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Walking_State : StateMachineBehaviour
// {
//     public GameObject NPC;
//     private Vector3 destination;
//     private float timeUntilMove = 0f;
//     private float timeUntilTurn = 0f;
//     private float NPC_half_width = 0.5f;


//     override public void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
//     {

//         // NPC = GameObject.FindWithTag("NPC");
//         // anim.SetBool("is_walk", true);
//         Debug.Log("In walking state");

//         // RaycastHit2D ray = Physics2D.Raycast(NPC.transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
//     }

//     // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
//     override public void OnStateUpdate(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//         // update timers
//         timeUntilMove -= Time.deltaTime;
//         timeUntilTurn -= Time.deltaTime;

//         // have we encountered the player?
//         Vector3 NPCPos = NPC.transform.position;
//         // if we see the player
//         if (PlayerWithin(NPCPos, 3f)) {
//             // Debug.Log("I see the player!");
//             anim.SetBool("player_seen", true);
//             anim.SetBool("is_walk", false);

//             // if we are near enough to attack the player
//             if (PlayerWithin(NPCPos, 1)) {
//                 // Debug.Log("ATTACK THE PLAYER");
//                 anim.SetTrigger("attack");
//             }
//         }

//         // checking for edges
//         if (timeUntilTurn <= 0 && !CheckRays(NPCPos.x, NPCPos.y, NPC_half_width + 1.3f)) {
//             destination = new Vector3(NPCPos.x * -1, NPCPos.y, 0);
//         }
        
//         // bool to see if the x destination value has already been reached
//         bool closeEnough = (Mathf.Abs(NPC.transform.position.x - destination.x) < .3);

//         // Direct the NPC to a random x position
//         if (timeUntilMove <= 0 || closeEnough) {
//             destination = new Vector3(Random.Range(-8, 8), NPCPos.y, 0);
//             timeUntilMove = Random.Range(5, 10);
//         }

//         moveNPCTo(destination);
//     }

//     void moveNPCTo(Vector3 transform)
//     {
//         NPC.GetComponent<NPCController>().moveToLocation(transform);
//     }

//     private bool PlayerWithin(Vector3 NPCPos, float distance) 
//     {
//         return (Physics2D.Raycast(NPCPos, Vector2.left, distance, LayerMask.GetMask("Player")).collider != null);
//     }

//     // function returns 0 if both are collided, 1 if left collided, 2 if right collided
//     // used to see if grounded on platform or near a ledge and must turn around
//     private bool CheckRays(float NPCxPos, float NPCyPos, float offset) 
//     {
//         bool leftHit = (Physics2D.Raycast(new Vector3(NPCxPos - offset, NPCyPos, 0), Vector2.down, 1f).collider != null);
//         bool rightHit = (Physics2D.Raycast(new Vector3(NPCxPos + offset, NPCyPos, 0), Vector2.down, 1f).collider != null);

//         if (leftHit && rightHit) {
//             return true;
//         }
//         timeUntilTurn = 4;
//         return false;
        
//     }

//     // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
//     override public void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//         anim.SetBool("is_walk", false);
//     }

//     // OnStateMove is called right after Animator.OnAnimatorMove()
//     //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     //{
//     //    // Implement code that processes and affects root motion
//     //}

//     // OnStateIK is called right after Animator.OnAnimatorIK()
//     //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     //{
//     //    // Implement code that sets up animation IK (inverse kinematics)
//     //}
// }
