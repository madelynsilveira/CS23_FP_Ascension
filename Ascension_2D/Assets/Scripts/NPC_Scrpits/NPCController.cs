using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Animator anim;
    public GameObject player;

    // movement variables
    public float speed = 4f;
    bool faceRight = true;
    private float timeUntilMove = 0f;
    // private float timeUntilTurn = 0f;

    public Vector3 targetLocation;
    public Transform playerTransform;



    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        playerTransform = GameObject.FindWithTag("Player").transform;

        // find a random target location for initial prowling
        targetLocation = eitherDirection(this.transform.position);
        anim.SetBool("npc_prowling", true);
    }


    void Update()
    {
        // updates
        timeUntilMove -= Time.deltaTime;
        targetLocation = getCurrTargetLocation(transform.position);
        checkTurning(targetLocation);
        checkHealing();

        // bool to see if the x destination value has already been reached
        bool closeEnough = (Mathf.Abs(transform.position.x - targetLocation.x) < .3);

        // Direct the NPC to a random x direction
        if (timeUntilMove <= 0 || closeEnough) {
            targetLocation = eitherDirection(transform.position);
            timeUntilMove = Random.Range(5, 10);
        }
        moveToLocation(targetLocation);
    }

    // checks for healing trigger and transitions to NPCHealed script
    private void checkHealing() {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            anim.SetTrigger("npc_healing");
            anim.SetBool("npc_following", true);
        }
        // create particle effect
    }

    public void moveToLocation(Vector3 targetLocation) {
        //Moves the character to the target location
        transform.position = Vector2.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
        if (transform.position == targetLocation)
        {
            // Debug.Log("Reached my position");
            if (anim.GetBool("npc_healed")) {
                anim.SetBool("npc_following", false);
            } else {
                anim.SetBool("npc_prowling", false);
            }
        } else // the npc is moving
        {
            if (anim.GetBool("npc_healed")) {
                anim.SetBool("npc_prowling", true);
            } else {
                anim.SetBool("npc_prowling", true);
            }
        }
    }

    // target location either player or random assigned elsewhere
    public Vector3 getCurrTargetLocation(Vector3 currentPos)
    {
        // need to go to a random location
        if (anim.GetBool("npc_prowling")) {
            return targetLocation;
            // return new Vector3(currentPos.x + Random.Range(-10, 11), currentPos.y, currentPos.z);
        // need to follow the player's transform
        } else if (anim.GetBool("npc_following") || anim.GetBool("npc_pursuing")) {
            return playerTransform.position;
        } else {
        // stay where it is
            // Debug.Log("idling");
            return currentPos;
        }
    }


    //If the character is not facing the target location, turn around
    private void checkTurning(Vector3 moveTowards) {
        if ((moveTowards.x < this.transform.position.x) && faceRight)
        {
            turnAround();
        }
        else if ((moveTowards.x > this.transform.position.x) && !faceRight)
        {
            turnAround();
        }
    }

    private void turnAround() {
        faceRight = !faceRight; // avoids constant turning

        // Multiply player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void changeDirection() {
        turnAround();
        targetLocation = new Vector3(targetLocation.x * -1, targetLocation.y, targetLocation.z);
        Debug.Log("turning!");
    }

    public bool playerWithin(float distance) 
    {
        bool left = (Physics2D.Raycast(transform.position, Vector2.left, distance, LayerMask.GetMask("Player")).collider != null);
        bool right = (Physics2D.Raycast(transform.position, Vector2.right, distance, LayerMask.GetMask("Player")).collider != null);
        return left || right;
    }

    // allows other scripts to set target location
    public void setTargetLocation(Vector3 location)
    {
        targetLocation = location;
    }

    // allows other scripts to set NPC speed
    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // allows other scripts to set NPC speed
    public float getSpeed()
    {
        return speed;
    }
    
    private Vector3 eitherDirection(Vector3 currentPos) {
        int coin = Random.Range(2, 4);
        if (coin % 2 == 0) {
            return new Vector3(currentPos.x + 10, currentPos.y, currentPos.z);
        } else {
            return new Vector3(currentPos.x - 10, currentPos.y, currentPos.z);
        }
    }

}

    // public void OnCollisionEnter2D(Collision2D other){
    //         if (other.gameObject.tag == "NPC") {
    //                 targetLocation = new Vector3(targetLocation.x * -1, targetLocation.y, targetLocation.z)
    //         }
    // }

    // public void OnCollisionExit2D(Collision2D other){
    //         if (other.gameObject.tag == "NPC") {
    //                 isAttacking = false;
    //                 //anim.SetBool("Attack", false);
    //         }
    // }

    // think it would be cool to glow the NPC when they walk by life
    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     //If the character found food, then make the food object a child
    //     if (col.gameObject.tag == "Life")
    //     {
    //         anim.SetInteger("hold_food", (anim.GetInteger("hold_food") + 1));
    //         //Debug.Log("NPC found food, currently holding " + anim.GetInteger("hold_food"));
    //         col.transform.parent = this.transform;
    //     }

    //     //If the character found the player, then set the player_near parameter in the state machine to true
    //     //May or may not trigger a transition
    //     if (col.gameObject.tag == "Player")
    //     {
    //         //Debug.Log("Player is near NPC");
    //         anim.SetBool("player_near", true);
    //     }
    // }
    // void OnTriggerExit2D(Collider2D col)
    // {
    //     if (col.gameObject.tag == "Player")
    //     {
    //         //Debug.Log("Player is not near NPC");
    //         anim.SetBool("player_near", false);
    //     }
    // }

    // private void OnTriggerStay2D(Collider2D col)
    // {
    //     if (col.gameObject.tag == "Player")
    //     {
    //         //Debug.Log("Player is near NPC");
    //         anim.SetBool("player_near", true);
    //     }
    // }

    // for following sprite transition: 
    //  if (GUI.Button(buttonPos, "Choose next sprite"))
    //     {
    //         spriteVersion += 1;
    //         if (spriteVersion > 3)
    //             spriteVersion = 0;
    //         spriteR.sprite = sprites[spriteVersion];
    //     }

