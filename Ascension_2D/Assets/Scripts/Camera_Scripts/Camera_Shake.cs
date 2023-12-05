using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    // public GameObject NPC;
    // public GameObject player;
    // public float knockBackForce = .000005f;

    public bool start = false;
    public AnimationCurve animCurve;
    private float shakeDuration = 0.5f;
    
    void Start() {
        NPC = GameObject.FindWithTag("NPC");
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHeal.beingAttacked) {
            PlayerHeal.beingAttacked = false;
            StartCoroutine(CamShake());

            // // player knockback
            // Rigidbody2D pushRB = player.GetComponent<Rigidbody2D>();
            // Vector2 moveDirectionPush = NPC.transform.position - player.transform.position;
            // pushRB.AddForce(moveDirectionPush.normalized * knockBackForce * - 1f, ForceMode2D.Impulse);
            // StartCoroutine(EndKnockBack(pushRB));
        }
    }

    public IEnumerator CamShake(){
        
        Vector3 startCamPos = transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration) {
            elapsedTime += Time.deltaTime;
            float strength = animCurve.Evaluate(elapsedTime / shakeDuration);
            transform.position = startCamPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startCamPos;
    }

    // // end the player knockback after npc attack
    // IEnumerator EndKnockBack(Rigidbody2D otherRB){
    //         yield return new WaitForSeconds(0.2f);
    //         otherRB.velocity= new Vector3(0,0,0);
    // }
}
