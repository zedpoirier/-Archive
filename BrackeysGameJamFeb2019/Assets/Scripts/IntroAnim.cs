using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroAnim : MonoBehaviour {

    public float animDelay = 5;
    public float timer;
    public float buffer = 0.25f;
    public bool fading = false;
    public Color alpha;
    public Text text;
    public Image img;
    public PlayerMovement player;


    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        alpha = new Color(0, 0, 0, 0);
        if (GetComponent<Text>() != null)
        {
            text = GetComponent<Text>();
        }
        else if(GetComponent<Image>() != null)
        {
            img = GetComponent<Image>();
        }
     }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (!fading)
        {
            if (timer >= animDelay || Input.anyKeyDown)
            {
                fading = true;
                timer = 0;
            }
        }
        else if (fading)
        {
            Fading();
        }
	}

    void Fading()
    {
        if (text)
        {
            text.color = Color.Lerp(text.color, alpha, timer * buffer);
            
        }
        else if (img)
        {
            img.color = Color.Lerp(img.color, alpha, timer * buffer);
        }

        if (timer * buffer >= 0.5f)
        {
            player.UnStop();
            Destroy(this);
        }
    }
}
