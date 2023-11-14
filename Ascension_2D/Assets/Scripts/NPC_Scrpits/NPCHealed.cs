using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealed : StateMachineBehaviour
{
    public GameObject NPC;


    override public void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {

        NPC = GameObject.FindWithTag("NPC");
        Debug.Log("Entered NPC HEALED");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving NPC healed");
    }

}
