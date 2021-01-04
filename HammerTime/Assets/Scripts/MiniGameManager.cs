using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Minigame
{
    Nails,
    Eggs,
    Calls
}

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;

    private void Awake()
    {
        // Singleton
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public float gameTimer;
    public float gameTime = 60f;
    public bool gameActive;
    public Minigame currentGame;

    [Header("Game Canvases")]
    public GameObject nailGame;
    public GameObject eggGame;
    public GameObject callGame;
    public GameObject scoreBoard;

    [Header("Scoreboard Properties")]
    public TMP_Text nailsText;
    public int nailsScore;
    public TMP_Text eggsText;
    public int eggsScore;
    public TMP_Text callsText;
    public int callsScore;


    private void Update()
    {
        if (gameActive)
        {
            gameTimer += Time.deltaTime;
            if (gameTimer > gameTime)
            {
                EndGame();
            }
        }
        //if (Input.GetKeyDown(KeyCode.Alpha1)) UpdateScoreBoard(Minigame.Nails, nailsScore + 1);
        //if (Input.GetKeyDown(KeyCode.Alpha2)) UpdateScoreBoard(Minigame.Eggs, eggsScore + 1);
        //if (Input.GetKeyDown(KeyCode.Alpha3)) UpdateScoreBoard(Minigame.Calls, callsScore + 1);
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    public void StartGame(Minigame game)
    {
        gameTimer = 0f;
        gameActive = true;
        currentGame = game;
        scoreBoard.SetActive(false);
        switch (game)
        {
            case Minigame.Nails:
                nailGame.SetActive(true);
                eggGame.SetActive(false);
                callGame.SetActive(false);
                break;
            case Minigame.Eggs:
                nailGame.SetActive(false);
                eggGame.SetActive(true);
                callGame.SetActive(false);
                break;
            case Minigame.Calls:
                nailGame.SetActive(false);
                eggGame.SetActive(false);
                callGame.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void EndGame()
    {
        gameActive = false;
        nailGame.SetActive(false);
        eggGame.SetActive(false);
        callGame.SetActive(false);
        scoreBoard.SetActive(true);
        switch (currentGame)
        {
            case Minigame.Nails: UpdateScoreBoard(Minigame.Nails, nailsScore);
                break;
            case Minigame.Eggs: UpdateScoreBoard(Minigame.Eggs, eggsScore);
                break;
            case Minigame.Calls: UpdateScoreBoard(Minigame.Calls, callsScore);
                break;
            default:
                break;
        }
    }

    public void UpdateScoreBoard(Minigame game, int score)
    {
        switch (game)
        {
            case Minigame.Nails:
                if (score > nailsScore) nailsScore = score;
                nailsText.text = string.Format("Nails: {0} in 60sec", nailsScore);
                break;
            case Minigame.Eggs:
                if (score > eggsScore) eggsScore = score;
                eggsText.text = string.Format("Eggs: {0} in 60sec", eggsScore);
                break;
            case Minigame.Calls:
                if (score > callsScore) callsScore = score;
                callsText.text = string.Format("Calls: {0} in 60sec", callsScore);
                break;
            default:
                break;
        }
    }
}
