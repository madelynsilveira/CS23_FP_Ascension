using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemy : StateMachineBehaviour
{
    public GameObject NPC;
    private GameObject Player;
    private Vector3 randomTarget;
    private Vector3 playerPosition;

    private float eyesight = 3f;
    private float attackRange = 1f;


    override public void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered NPC ENEMY");
        
        NPC = GameObject.FindWithTag("NPC");
        playerPosition = GameObject.FindWithTag("Player").transform.position;
        
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 NPCPos = NPC.transform.position;

        // have we encountered the player?
        if (playerWithin(eyesight)) { 
            // switch from prowl to pursue
            // Debug.Log("I see the player");
            anim.SetBool("npc_prowling", false);
            anim.SetBool("npc_pursuing", true);

            // switch from pursue to attack
            if (playerWithin(attackRange)) {
                // Debug.Log("ATTACK THE PLAYER");
                anim.SetBool("npc_prowling", false);
                anim.SetBool("npc_attacking", true);
            }
        } else {
            anim.SetBool("npc_prowling", true);
            anim.SetBool("npc_pursuing", false);
            anim.SetBool("npc_attacking", false);
        }

        // set the NPC's target (random, player, or idle)
        if (anim.GetBool("npc_prowling") || anim.GetBool("npc_pursuing")) {
            NPC.GetComponent<NPCController>().getCurrTargetLocation(NPCPos);
        } 


    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.SetBool("npc_prowling", false);
    }


    private bool playerWithin(float distance) 
    {
        return NPC.GetComponent<NPCController>().playerWithin(distance);
        // return (Physics2D.Raycast(NPCPos, Vector2.left, distance, LayerMask.GetMask("Player")).collider != null);
    }


    

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
