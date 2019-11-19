using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgeDataGrabber : MonoBehaviour {

    public string name;
    public Text lineText;
    public bool hasBeenSet = false;
    public AgeCalculator calculator;

	// Use this for initialization
	void Start () {
        lineText = GetComponent<Text>();
        calculator = FindObjectOfType<AgeCalculator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasBeenSet)
        {
            if (name == "rhea")
            {
                lineText.text = ("Rhea's age in days is " + calculator.rheaAge.ToString());
                hasBeenSet = true;
            }
            else if (name == "brandi")
            {
                lineText.text = ("Brandi's age in days is " + calculator.brandiAge.ToString());
                hasBeenSet = true;
            }
            else if (name == "zed")
            {
                lineText.text = ("Zed's age in days is " + calculator.zackAge.ToString());
                hasBeenSet = true;
            }
        }
	}
}
