using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Bottom_Script : MonoBehaviour
{
    public static Text textComponent;

    public static int currIndex;
    public static string[] messages = {
        "Oh no! You have fallen from grace.",
        "Follow the blue orbs, but beware of demons..", 
        "The orbs increase your soul bar up top!",
        "Use souls to heal yourself with the down arrow.", 
        "Check out how many times you can jump!", 
        "Who is that scary creature? Try to get a closer look.",
        "Use shift or 'e' to heal the demon if it gets close.",
        "Healing all demons in a level gives you a star.",
        "This key might help you open something...",
        "Keep going until you reach the gate!"
    };

    void Start()
    {
        // Hide the text initially
        textComponent = GameObject.FindWithTag("Instructions").GetComponent<Text>();
        textComponent.enabled = false;
        currIndex = 0;
    }

    void Update()
    {
    }
    
    public static void DisplayText(string message)
    {
        textComponent.text = message;
        textComponent.enabled = true;
    }

}
