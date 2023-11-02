using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("down")) {
            Debug.Log("Player hiding");
        } else if (Input.GetKeyUp("down")) {
            Debug.Log("Player stopped hiding");
        }

    }
}
