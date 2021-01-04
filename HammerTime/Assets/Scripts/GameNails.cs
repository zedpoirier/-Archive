using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameNails : MonoBehaviour
{
    [Header("General Properties")]
    public Minigame game;
    public TMP_Text timer;
    public TMP_Text scoreText;
    public int highScore = 0;
    public int currentScore = 0;

    [Header("Nails Game Properties")]
    public GameObject workTable;
    public GameObject nailObject;
    public Transform[] starterNails; // bl, br, tr, tl
    public Glue glueBottle;
    public GlueSpray glueSpray;
    public int nailCount;
    public int maxUnhammeredNailCount = 4;
    public float nailTimer;
    public float nailTime;
    public Vector2 nailTimeRange;
    public bool spawningNails;

    private void Start()
    {
        for (int i = 0; i < starterNails.Length; i++)
        {
            starterNails[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (MiniGameManager.instance.gameActive == false) EndGame();
        float timeFull = 60f - MiniGameManager.instance.gameTimer;
        float time = (float)Mathf.Round(timeFull * 100f) / 100f;
        timer.text = "Time Left: " + time.ToString("0.00") + "s";
        scoreText.text = "Nails Hammered: " + currentScore.ToString();
        if (spawningNails) SpawnNails();
    }

    public void StartGame()
    {
        MiniGameManager.instance.StartGame(game);
        currentScore = 0;
        nailCount = 0;
        spawningNails = true;
        nailTime = Random.Range(nailTimeRange.x, nailTimeRange.y);
    }

    void SpawnNails()
    {
        if (nailCount < maxUnhammeredNailCount)
        {
            nailTimer += Time.deltaTime;
            if (nailTimer > nailTime)
            {
                float xPos = Random.Range(starterNails[0].position.x, starterNails[3].position.x);
                float zPos = Random.Range(starterNails[0].position.z, starterNails[1].position.z);
                Vector3 spawnPosition = new Vector3(xPos, starterNails[0].position.y, zPos);
                GameObject spawnedNail = Instantiate(nailObject, spawnPosition, Quaternion.identity, transform);
                spawnedNail.SetActive(true);
                nailTime = Random.Range(nailTimeRange.x, nailTimeRange.y);
                nailCount++;
            }
        }
        else
        {
            nailTimer = 0f;
        }
    }

    public void UpdateScore()
    {
        currentScore++;
        nailCount--;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            MiniGameManager.instance.nailsScore = highScore;
        }
    }

    void EndGame()
    {
        spawningNails = false;
        glueBottle.sprayed = false;
    }
}
