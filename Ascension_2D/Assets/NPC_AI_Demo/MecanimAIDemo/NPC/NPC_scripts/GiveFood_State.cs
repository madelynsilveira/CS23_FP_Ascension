using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveFood_State : StateMachineBehaviour
{
    public GameObject NPC;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = GameObject.FindWithTag("NPC");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if the NPC has food, then give food to player
        if (animator.GetInteger("hold_food") > 0)
        {
            //Remove every food object the NPC is holding
            foreach (Transform transform in NPC.transform)
            {
                if (transform.CompareTag("Food"))
                {
                    transform.gameObject.SetActive(false);
                }
            }
            animator.SetInteger("hold_food", 0);
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
