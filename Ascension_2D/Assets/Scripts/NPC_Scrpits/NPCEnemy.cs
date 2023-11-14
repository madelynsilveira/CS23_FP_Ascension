using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemy : StateMachineBehaviour
{
    public GameObject NPC;


    override public void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {

        NPC = GameObject.FindWithTag("NPC");
        // anim.SetBool("npc_prowl", true);
        Debug.Log("Entered NPC ENEMY");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving NPC enemy");
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
