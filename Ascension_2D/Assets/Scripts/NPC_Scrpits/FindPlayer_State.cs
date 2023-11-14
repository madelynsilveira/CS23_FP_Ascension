using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer_State : StateMachineBehaviour
{
    public GameObject NPC;
    public GameObject player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Sets the NPC to follow the player
        NPC = GameObject.FindWithTag("NPC");
        anim = NPC.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        NPC.GetComponent<NPCController>().followPosition(player.transform);
        Debug.Log("In find player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 NPCPos = NPC.transform.position;
        if (PlayerWithin(NPCPos, 1)) {
            Debug.Log("ATTACK THE PLAYER");
            anim.SetBool("npc_attack", true);
        } else if (PlayerWithin(NPCPos, 4)) {
            Debug.Log("I see the player");
            NPC.GetComponent<NPCController>().followPosition(player.transform);
        } else {
            // player has escaped
            Debug.Log("where did you go?");
            anim.SetBool("player_seen", false);
            // anim.SetBool("is_walk", true);
        }
    }

    override public void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // NPC.GetComponent<NPCController>().moveToLocation(player.transform.position);
        anim.SetBool("player_seen", false);
        // anim.SetBool("is_walk", true);
    }

    private bool PlayerWithin(Vector3 NPCPos, float distance) 
    {
        return (Physics2D.Raycast(NPCPos, Vector2.left, distance, LayerMask.GetMask("Player")).collider != null);
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
