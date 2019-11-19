using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {

    public SpriteRenderer sprite;
    public Color color;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        float red = Random.Range(0.0f, 1.0f);
        float green = Random.Range(0.0f, 1.0f);
        float blue = Random.Range(0.0f, 1.0f);
        color = new Color(red, green, blue);
        sprite.color = color;
	}
	
}
