using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditAnim : MonoBehaviour {

    public float timer;
    public float buffer = 0.25f;
    public bool revealing = false;
    public Color alpha;
    public Text text;
    public Image img;
    public Vector3 translation;


    // Use this for initialization
    void Start()
    {
        alpha = new Color(0, 0, 0, 0);
        if (GetComponent<Text>() != null)
        {
            text = GetComponent<Text>();
            text.color = alpha;
        }
        else if (GetComponent<Image>() != null)
        {
            img = GetComponent<Image>();
            img.color = alpha;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (revealing)
        {
            timer += Time.deltaTime;
            Reveal();
        }
    }

    void Reveal()
    {
        if (text)
        {
            text.color = Color.Lerp(alpha, Color.white, timer* buffer);
        }
        else if (img)
        {
            img.color = Color.Lerp(alpha, Color.black, timer * buffer);
        }

        if (timer * buffer >= 1f)
        {
            transform.Translate(translation * Time.deltaTime);
        }
    }
}
