using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking_State : StateMachineBehaviour
{
    public GameObject NPC;
    
    float timeUntilMove;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Find one food gameObject present at the start of the behaviour
        NPC = GameObject.FindWithTag("NPC");

        // Find the platform they are standing on
        Vector2 raycastPosition = NPC.transform.position + Vector3.up * 3;
        RaycastHit2D hit = Physics2D.Raycast(raycastPosition, Vector2.down, 1f, LayerMask.GetMask("PlatformLayer"));

        if (hit.collider != null)
        {
            Debug.Log("hit a platform");
            GameObject collidedObject = hit.collider.gameObject;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 raycastPosition = NPC.transform.position + Vector3.up * 1;
        RaycastHit2D hit = Physics2D.Raycast(raycastPosition, 
                            Vector2.down, 1f, LayerMask.GetMask("PlatformLayer"));
        if (hit.collider != null)
        {
            Debug.Log("hit a platform");
            GameObject collidedObject = hit.collider.gameObject;
        }

        timeUntilMove -= Time.deltaTime;
        if (timeUntilMove <= 0)
        {
            //Direct the NPC to a random position in the scene
            moveNPCTo(new Vector3(Random.Range(-8, 8), Random.Range(-3, 3), 0));
            //Debug.Log("Moving to " + startPos);
            timeUntilMove = Random.Range(10, 15);
        }
    }

    void moveNPCTo(Vector3 transform)
    {
        NPC.GetComponent<NPCController>().moveToLocation(transform);
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
