using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Animator anim;
    public GameObject player;

    // movement variables
    public float speed = 5f;
    bool faceRight = true;
    private float timeUntilMove = 0f;
    // private float timeUntilTurn = 0f;

    public Vector3 targetLocation;
    public Transform playerTransform;


    // audio
    // public AudioSource[3];
    // public AudioSource attack_smallSFX;
    // public AudioSource attack_mediumSFX;
    // public AudioSource attack_bigSFX;
    // if (jumpSFX.isPlaying == false){
    //                     jumpSFX.Play();
    //             }

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        playerTransform = GameObject.FindWithTag("Player").transform;

        // find a random target location for initial prowling
        Vector3 cp = this.transform.position;
        targetLocation = new Vector3(cp.x + Random.Range(-10, 11), cp.y, cp.z);
        anim.SetBool("npc_prowling", true);
    }

    // Update is called once per frame
    void Update()
    {
        // updates
        timeUntilMove -= Time.deltaTime;
        targetLocation = getCurrTargetLocation();
        checkTurning(targetLocation);

        // bool to see if the x destination value has already been reached
        bool closeEnough = (Mathf.Abs(transform.position.x - targetLocation.x) < .3);

        // Direct the NPC to a random x position
        if (timeUntilMove <= 0 || closeEnough) {
            targetLocation = new Vector3(Random.Range(-10, 11), transform.position.y, 0);
            timeUntilMove = Random.Range(5, 10);
        }

        moveToLocation(targetLocation);


        
    }

    public void moveToLocation(Vector3 targetLocation) {
        //Moves the character to the target location
        transform.position = Vector2.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
        if (transform.position == targetLocation)
        {
            Debug.Log("Reached my position");
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
    private Vector3 getCurrTargetLocation()
    {
        if (anim.GetBool("npc_prowling")) {
            return targetLocation;
        } else if (anim.GetBool("npc_following") || anim.GetBool("npc_pursuing")) {
            return playerTransform.position;
        } else {
            Debug.Log("something else");
            return targetLocation;
        }
    }

    // allows other scripts to set target location
    public void setTargetLocation(Vector3 location)
    {
        targetLocation = location;
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
}
