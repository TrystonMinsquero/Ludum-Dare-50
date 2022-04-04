using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Line
{
    [TextArea(1,3)]
    public string text;
    public float duration = 3f;
}
public class TutorialGuide : MonoBehaviour
{
    public Canvas speechBubble;
    public Text speechText;

    public Line[] lines;

    public void Say(string text)
    {
        Debug.Log(text);
        speechText.text = text;
        speechBubble.enabled = true;
    }

    public void Speak()
    {
        speechBubble.enabled = true;
        StartCoroutine(SayLines());
    }
    
    private IEnumerator SayLines()
    {
        foreach (var line in lines)
        {
            speechText.text = line.text;
            // Debug.Log(line.text);
            yield return new WaitForSeconds(line.duration);
        }
        Speak();
    }

    public void ShutUp()
    {
        // Debug.Log("Shut up");
        speechBubble.enabled = false;
        StopAllCoroutines();
    }
}
