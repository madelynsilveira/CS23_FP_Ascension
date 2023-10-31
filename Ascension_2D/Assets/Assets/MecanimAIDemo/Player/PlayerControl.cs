using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D m_Rigidbody;
    public Animator anim;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        m_Rigidbody.MovePosition(transform.position + speed * Time.deltaTime * m_Input);

    }

    void Update()
    {
        //flip character based on movement direction
        if (Input.GetAxis("Horizontal") < 0) {
            anim.SetBool("player_walk", true);
            Vector3 newScale = transform.localScale;
            newScale.x = -1.0f;
            transform.localScale = newScale;
        } else if (Input.GetAxis("Horizontal") > 0) {
            anim.SetBool("player_walk", true);
            Vector3 newScale = transform.localScale;
            newScale.x = 1.0f;
            transform.localScale = newScale;
        } else if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") < 0) {
            anim.SetBool("player_walk", true);
        } else {
            anim.SetBool("player_walk", false);
        }
    }
}

