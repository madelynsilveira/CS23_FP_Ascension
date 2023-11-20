using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public GameObject player;
    public bool isHidden;
    public Transform feet;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public static bool canHide;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        isHidden = false;
    }

    // Update is called once per frame
    void Update()
    {
        // SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();

        if (canHide && !isHidden && (Input.GetKey("down") || Input.GetKey("s")) && IsGrounded()) {
            Debug.Log("Player hiding");
            isHidden = true;
            // playerSprite.color = Color.blue;
            PlayerMove.isFrozen = true;
            // animate crouch
            player.layer = LayerMask.NameToLayer("PlayerHidden");
        } else if (isHidden && (Input.GetKeyUp("down") || (Input.GetKeyUp("s")))) {
            Debug.Log("Player stopped hiding");
            isHidden = false;
            PlayerMove.isFrozen = false;
            // get out of crouch
            // playerSprite.color = Color.red;
            player.layer = LayerMask.NameToLayer("Player");
        }

    }

    public bool IsGrounded() {
            Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 2f, groundLayer);
            Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 2f, enemyLayer);
            if ((groundCheck != null) || (enemyCheck != null)) {
                  return true;
            }
            return false;
      }
}
