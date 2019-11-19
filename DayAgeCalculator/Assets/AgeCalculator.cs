using UnityEngine;
using System;

public class AgeCalculator : MonoBehaviour {

    public int rheaAge = 0;
    public int brandiAge = 0;
    public int zackAge = 0;

    private DateTime today = DateTime.Today;
    private DateTime rDay = new DateTime(2017, 2, 21);
    private DateTime bDay = new DateTime(1992, 7, 2);
    private DateTime zDay = new DateTime(1993, 3, 8);
   
	// Use this for initialization
	void Start () {
        rheaAge = CalcAge(rDay);
        brandiAge = CalcAge(bDay);
        zackAge = CalcAge(zDay);
    }
	
    public int CalcAge(DateTime day)
    {
        int age = (today - day).Days;
        return age;
    }
}
