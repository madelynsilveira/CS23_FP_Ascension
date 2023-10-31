using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 targetLocation;
    public Transform followTransform;
    Vector3 moveTowards;
    public bool followTarget;

    bool faceRight = true;
    

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        followTarget = true;
        targetLocation = this.transform.position;
        followTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveTowards = getCurrTargetLocation();
        
        //Moves the character to the target location
        transform.position = Vector2.MoveTowards(transform.position, moveTowards, speed * Time.deltaTime);
        if (transform.position == moveTowards)
        {
            anim.SetBool("is_walk", false);
        } else
        {
            anim.SetBool("is_walk", true);
        }
        
        //If the character is not facing the target location, turn around
        if ((moveTowards.x < this.transform.position.x) && faceRight)
        {
            turnAround();
        }
        else if ((moveTowards.x > this.transform.position.x) && !faceRight)
        {
            turnAround();
        }
    }

    //Chooses the current target location based on the type of location given (Transform or Vector3 transform.location)
    private Vector3 getCurrTargetLocation()
    {
        if (followTarget) {
            return followTransform.position;
        } else {
            return targetLocation;
        }
    }

    //Set the character to move towards a specific location
    //Takes in Vector3 gameObject.transform.location
    public void moveToLocation(Vector3 location)
    {
        followTarget = false;
        targetLocation = location;
    }

    //Set the character to follow another gameObject's transform
    //Takes in Transform gameObject.transform
    //Note: if the gameObject leaves its previous location, then the target location will move too
    public void followPosition(Transform transform)
    {
        followTarget = true;
        followTransform = transform;
    }

    private void turnAround()
    {
        // NOTE: Switch player facing label (avoids constant turning)
        faceRight = !faceRight;

        // NOTE: Multiply player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //If the character found food, then make the food object a child
        if (col.gameObject.tag == "Food")
        {
            anim.SetInteger("hold_food", (anim.GetInteger("hold_food") + 1));
            //Debug.Log("NPC found food, currently holding " + anim.GetInteger("hold_food"));
            col.transform.parent = this.transform;
        }

        //If the character found the player, then set the player_near parameter in the state machine to true
        //May or may not trigger a transition
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("Player is near NPC");
            anim.SetBool("player_near", true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("Player is not near NPC");
            anim.SetBool("player_near", false);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("Player is near NPC");
            anim.SetBool("player_near", true);
        }
    }
}
