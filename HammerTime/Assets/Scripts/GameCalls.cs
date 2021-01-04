using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCalls : MonoBehaviour
{
    [Header("General Properties")]
    public Minigame game;
    public TMP_Text timer;
    public TMP_Text scoreText;
    public int highScore = 0;
    public int currentScore = 0;
    public TMP_Text display;

    [Header("Calls Game Properties")]
    public List<int> targetNumber = new List<int>(10);
    public List<int> currentNumber;

    private void Update()
    {
        if (MiniGameManager.instance.gameActive == false) ResetDisplay();
        float timeFull = 60f - MiniGameManager.instance.gameTimer;
        float time = (float)Mathf.Round(timeFull * 100f) / 100f;
        timer.text = "Time Left: " + time.ToString("0.00") + "s";
        scoreText.text = "Calls Completed: " + currentScore.ToString();
    }

    void NewNumber()
    {
        currentNumber = new List<int>();
        RandomizeTargetNumber();
        ChangeDisplay();
    }
    void RandomizeTargetNumber()
    {
        for (int i = 0; i < targetNumber.Count; i++)
        {
            targetNumber[i] = Random.Range(0, 10);
        }
    }

    void CheckNumber()
    {
        for (int i = 0; i < currentNumber.Count; i++)
        {
            if (currentNumber[i] != targetNumber[i])
            {
                NewNumber();
                // play losing sfx
                break;
            }

            if (i == 9) currentScore++;
            if (currentScore > highScore)
            {
                highScore = currentScore;
                MiniGameManager.instance.callsScore = highScore;
                NewNumber();
            }
        }
    }


    void ChangeDisplay()
    {
        display.text =
            targetNumber[0].ToString() +
            targetNumber[1].ToString() +
            targetNumber[2].ToString() +
            "-" +
            targetNumber[3].ToString() +
            targetNumber[4].ToString() +
            targetNumber[5].ToString() +
            "-" +
            targetNumber[6].ToString() +
            targetNumber[7].ToString() +
            targetNumber[8].ToString() +
            targetNumber[9].ToString();
    }

    void ResetDisplay()
    {
        display.text = "000-000-0000";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PhoneButton>())
        {
            if (MiniGameManager.instance.gameActive)
            {
                currentNumber.Add(other.GetComponent<PhoneButton>().buttonNum);
                CheckNumber();
            }
            else
            {
                if(other.GetComponent<PhoneButton>().buttonNum == 0)
                {
                    MiniGameManager.instance.StartGame(game);
                    currentScore = 0;
                    NewNumber();
                }
            }
            Debug.Log(other.GetComponent<PhoneButton>().buttonNum);
        }
    }
}
