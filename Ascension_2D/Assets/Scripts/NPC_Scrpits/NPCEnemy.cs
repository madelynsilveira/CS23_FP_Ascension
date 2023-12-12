using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemy : StateMachineBehaviour
//public class NPCEnemy : MonoBehaviour
{
    public GameObject NPC;
    public GameObject player;
    //public Animator anim;
    private Vector3 randomTarget;
    private NPCController npcController;

    // player detection
    private float eyesight = 5f;
    private float attackRange = 1f;
    // private float normalSpeed = 0f;
    // private float doubleSpeed = 0f;
    private float timeSinceLastEdge = 0f;
    private AudioSource audioSource;

    // audio
    // public AudioSource[3];
    // public AudioSource attack_small_SFX;
    // public AudioSource attack_medium_SFX;
    // public AudioSource attack_big_SFX;



    override public void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Debug.Log("Entered NPC ENEMY");
        // npcController = anim.GetComponent<NPCController>();
        // NPC = anim.gameObject;
        NPC = GameObject.FindWithTag("NPC");
        player = GameObject.FindWithTag("Player");
        // Debug.Log ("NPC coordinates: " + NPC.transform.position.x + ", " + NPC.transform.position.y);
        
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 NPCPos = NPC.transform.position;
        timeSinceLastEdge -= Time.deltaTime;

        if ((timeSinceLastEdge <= 0 ) && (anim.GetBool("npc_prowling"))) {
            checkEdges(NPCPos);
            //checkEdges(transform.position);
        }

        // have we encountered the player?
        if (playerWithin(eyesight)) { 
            // switch from prowl to pursue
            anim.SetBool("npc_prowling", false);
            anim.SetBool("npc_pursuing", true);
            anim.SetBool("npc_attacking", false);
            NPC.GetComponent<NPCController>().setSpeed(8f);

            // SpriteRenderer NPCSpriteRenderer = NPC.GetComponentInChildren<SpriteRenderer>();
            // NPCSpriteRenderer.color = Color.red; // why is this not working?

            // switch from pursue to attack
            if (playerWithin(attackRange)) {
                //NPC.GetComponent<AudioSource>().Play();
                audioSource = NPC.GetComponent<AudioSource>();
                if (!audioSource.isPlaying) {
                    audioSource.Play();
                }
                // set up animation
                anim.SetBool("npc_prowling", false);
                anim.SetBool("npc_attacking", true);

                // decrease health somewhere else
                PlayerHeal.beingAttacked = true;
                Debug.Log("PlayerHeal.beingAttacked: " + PlayerHeal.beingAttacked);
                //Debug.Log("attacking");
              
                // if (attack_medium_SFX.isPlaying == false){
                //         attack_medium_SFX.Play();
                // }
            }
        } else {
            anim.SetBool("npc_prowling", true);
            anim.SetBool("npc_pursuing", false);
            anim.SetBool("npc_attacking", false);
            //PlayerHeal.beingAttacked = false;
            NPC.GetComponent<NPCController>().setSpeed(4f);
            //gameObject.GetComponent<NPCController>().setSpeed(4f);
        }

        // set the NPC's target (random, player, or idle)
        if (anim.GetBool("npc_prowling") || anim.GetBool("npc_pursuing")) {
            NPC.GetComponent<NPCController>().getCurrTargetLocation(NPCPos);
            //gameObject.GetComponent<NPCController>().getCurrTargetLocation(transform.position);
        } 


    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.SetBool("npc_prowling", false);
        anim.SetBool("npc_pursuing", false);
        anim.SetBool("npc_attacking", false);
        
    }


    private bool playerWithin(float distance) {
        return NPC.GetComponent<NPCController>().characterWithin(distance);
        //return gameObject.GetComponent<NPCController>().characterWithin(distance);
    }

    // allows the npc to stay on a platform while prowling
    private void checkEdges(Vector3 NPCPos) {
        // raycast down to the left and right to check for collision
        Vector3 posLeft = new Vector3(NPCPos.x - 1, NPCPos.y, NPCPos.z);
        Vector3 posRight = new Vector3(NPCPos.x + 1, NPCPos.y, NPCPos.z);
        bool leftRay = (Physics2D.Raycast(posLeft, Vector2.down, 2, LayerMask.GetMask("Ground")).collider != null);
        bool rightRay = (Physics2D.Raycast(posRight, Vector2.down, 2, LayerMask.GetMask("Ground")).collider != null);
        // call function to change the direction

        if ((leftRay && !rightRay) || (rightRay && !leftRay)) {
            
            //Debug.Log("EDge");
            NPC.GetComponent<NPCController>().changeDirection();
            //gameObject.GetComponent<NPCController>().changeDirection();
            timeSinceLastEdge = 1;
        } 

    }


}
