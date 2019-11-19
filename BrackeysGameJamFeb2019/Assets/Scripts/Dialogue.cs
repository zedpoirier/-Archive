using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    public enum DialogueType {Random, Prewritten}
    public DialogueType type;
    public Text text;
    public float waitTimeBetweenChar = 0.5f;
    public string currentMessage;
    public Talk talker;
    public float paddingTime = 1.5f;
    public AppearAfterComplete lover;

    [Header("Fade Settings")]
    public float fadeInterval = 0.05f;
    public Color backgroundColor;
    public Color startColor;
    public float timer;
    public bool fading = true;

    [Header("Random Settings")]
    public int sentenceLength = 5;
    public int minChar = 2;
    public int maxChar = 6;
    public RandomText randText;

    [Header("Prewritten Settings")]
    public List<string> lines;
    public int callCount = 0;
    public string lastMessage = "I have no more things to tell you";

    // Use this for initialization
    void Start () {
        text.text = "";
        randText = GetComponent<RandomText>();
        talker = GameObject.FindGameObjectWithTag("Player").GetComponent<Talk>();
        startColor = GetComponent<SpriteRenderer>().color;
        backgroundColor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().backgroundColor;
        lover = GameObject.FindGameObjectWithTag("Lover").GetComponent<AppearAfterComplete>();
    }

    public void Call()
    {
        if (type == DialogueType.Random)
        {
            currentMessage = randText.GenerateText(sentenceLength, minChar, maxChar);
            StartCoroutine("slowWrite");
        }
        else
        {
            if (callCount < lines.Count)
            {
                currentMessage = lines[callCount];
                StartCoroutine("slowWrite");
                callCount++;
            }
            else
            {
                currentMessage = lastMessage;
                StartCoroutine("slowWrite");
            }
        }
    }

    public IEnumerator slowWrite()
    {
        text.text = "";
        foreach (char c in currentMessage)
        {
            text.text += c;          
            yield return new WaitForSeconds(waitTimeBetweenChar);
        }
        yield return new WaitForSeconds(paddingTime);
        talker.zoomer.UnZoomOnConvo();
        talker.DisengageConversation();
        lover.checks++;
        yield return new WaitForSeconds(paddingTime / 2);
        StartCoroutine("FadeAway");
        if (lover.checks >= 4)
        {
            talker.zoomer.LoverZoom();
        }
    }

    IEnumerator FadeAway()
    {
        while (fading)
        {
            timer += Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, backgroundColor, timer);
            text.color = Color.Lerp(text.color, backgroundColor, timer);
            yield return new WaitForSeconds(fadeInterval);
            if (startColor == backgroundColor)
            {
                fading = false;
            }
        }
        
    }

}
