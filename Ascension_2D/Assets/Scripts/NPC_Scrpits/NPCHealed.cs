using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealed : StateMachineBehaviour
{
    public GameObject NPC;
    private float maxFollowingDistance = 15;
    private float minFollowingDistance = 5;

    override public void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {

        NPC = GameObject.FindWithTag("NPC");
        Debug.Log("Entered NPC HEALED");
        anim.SetBool("npc_healed", true);
        anim.SetBool("npc_following", true);
        anim.SetBool("npc_prowling", false);
        anim.SetBool("npc_pursuing", false);
        anim.SetBool("npc_attack", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 NPCPos = NPC.transform.position;
        NPC.GetComponent<NPCController>().getCurrTargetLocation(NPCPos);
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Leaving NPC healed");
        // anim.SetBool("npc_healed", false);
    }

    // private bool playerWithin(float distance) {
    //     return NPC.GetComponent<NPCController>().playerWithin(distance);
    // }

}
