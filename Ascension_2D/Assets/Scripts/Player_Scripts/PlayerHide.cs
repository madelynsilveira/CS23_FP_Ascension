using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    // public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        // SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();

        if (Input.GetKey("down")) {
            Debug.Log("Player hiding");
            // playerSprite.color = Color.blue;
            player.layer = LayerMask.NameToLayer("PlayerHidden");
        } else if (Input.GetKeyUp("down")) {
            Debug.Log("Player stopped hiding");
            // playerSprite.color = Color.red;
            player.layer = LayerMask.NameToLayer("Player");
        }

    }
}
