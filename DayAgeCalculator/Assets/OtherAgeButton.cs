using UnityEngine;
using UnityEngine.UI;
using System;

public class OtherAgeButton : MonoBehaviour {

    public Text otherText;
    public AgeCalculator calculator;

    public InputField day;
    public InputField month;
    public InputField year;

    private int dayVal;
    private int monthVal;
    private int yearVal;

    private DateTime otherDay;

	// Use this for initialization
	void Start () {
        day = GameObject.FindGameObjectWithTag("Day").GetComponent<InputField>();
        month = GameObject.FindGameObjectWithTag("Month").GetComponent<InputField>();
        year = GameObject.FindGameObjectWithTag("Year").GetComponent<InputField>();

        calculator = FindObjectOfType<AgeCalculator>();
	}
	
    public void CalculateYourAge()
    {
        dayVal = Convert.ToInt32(day.text);
        monthVal = Convert.ToInt32(month.text);
        yearVal = Convert.ToInt32(year.text);

        otherDay = new DateTime(yearVal, monthVal, dayVal);
        if (otherDay == null)
        {
            Debug.Log("Failed");
        }
        else
        {
            otherText.text = ("Your age in days is " + calculator.CalcAge(otherDay).ToString());
        }
    }
}
