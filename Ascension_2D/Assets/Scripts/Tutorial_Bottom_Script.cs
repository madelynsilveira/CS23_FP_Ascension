using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Bottom_Script : MonoBehaviour
{
    public Text textComponent;
    private string defaultMessage = "Oh no! You have fallen from grace..";
        // private string defaultMessage = "HHAGUGSJHGllen from grace..";
    private float fallTime = 2.0f;
    private float nextTime = 4.0f;
    private float thirdTime = 9.0f;
    private float fourthTime = 16.0f;
    private float fifthTime = 20.0f;
    private float sixthTime = 24.0f;
    private float seventhTime = 28.0f;
    private float eighthTime = 32.0f;
    // public float displayDuration = 1.0f; 

    // private bool isDisplaying = false;
    // private float displayTimer = 0.0f;

    // private int next = 0;
    // private string[] messages = {
    //     "Oh no! You have fallen from grace..",
    //     "Maybe you should follow the blue orbs..", 
    //     "They increase your soul bar up top!", 
    //     "Who is that scary creature?",
    //     "Try healing it!",
    //     "Keep going until you reach the gate!"
    // };

    void Start()
    {
        fallTime -= Time.deltaTime;
        nextTime -= Time.deltaTime;
        thirdTime -= Time.deltaTime;
        fourthTime -= Time.deltaTime;
        fifthTime -= Time.deltaTime;
        sixthTime -= Time.deltaTime;
        seventhTime -= Time.deltaTime;
        eighthTime -= Time.deltaTime;
        
        // Hide the text initially
        textComponent.enabled = false;
    }

    void Update()
    {
        fallTime -= Time.deltaTime;
        nextTime -= Time.deltaTime;
        thirdTime -= Time.deltaTime;
        fourthTime -= Time.deltaTime;
        fifthTime -= Time.deltaTime;
        sixthTime -= Time.deltaTime;
        seventhTime -= Time.deltaTime;
        eighthTime -= Time.deltaTime;

        if (fallTime <= 0)
        {
            DisplayText(defaultMessage);
        }

        if (nextTime <= 0)
        {
            DisplayText("Maybe you should follow the blue orbs..");
        }

        if (thirdTime <= 0)
        {
            DisplayText("They increase your soul bar up top!");
        }

        if (fourthTime <= 0) {
            DisplayText("Who is that scary creature?");
        }
        
        if (fifthTime <= 0) {
            DisplayText("Try healing it!");
        }

        if (sixthTime <= 0) {
            DisplayText("Now heal yourself!");
        }

        if (seventhTime <= 0) {
            DisplayText("Make sure you grab the key!");
        }

        if (eighthTime <= 0) {
            DisplayText("Keep going until you reach the gate!");
        }
    }

    void DisplayText(string message)
    {
        textComponent.text = message;
        // textComponent.text = "Maybe you should follow the blue orbs..";
        textComponent.enabled = true;
        // isDisplaying = true;
        // displayTimer = 0.0f;
    }

    // void HideText()
    // {
    //     textComponent.enabled = false;
    //     isDisplaying = false;
    // }

    // public void OnTriggerEnter2D (Collider2D other){
    //     if (other.gameObject.tag == "next"){
    //             GetComponent<Collider2D>().enabled = false;
    //             //GetComponent< AudioSource>().Play();
    //             StartCoroutine(DestroyThis());

    //             if (isHealthPickUp == true) {
    //                 gameHandler.playerGetHit(healthBoost * -1);
    //                 //playerPowerupVFX.powerup();
    //             }

    //             if (isSpeedBoostPickUp == true) {
    //                 other.gameObject.GetComponent<PlayerMove>().speedBoost(speedBoost, speedTime);
    //                 //playerPowerupVFX.powerup();
    //             }
    //     }
    // }
}
