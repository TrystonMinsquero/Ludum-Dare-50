using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGuide : MonoBehaviour
{
    public Canvas speechBubble;
    public Text speechText;

    public void Say(string text)
    {
        Debug.Log(text);
        speechText.text = text;
        speechBubble.enabled = true;
    }

    public void ShutUp()
    {
        Debug.Log("Shut up");
        speechBubble.enabled = false;
    }
}
