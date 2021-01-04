using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Stage
{
    Menu,
    Zen,
    Workshop,
    Kitchen,
    LivingRoom
}

public class GameStageManager : MonoBehaviour
{
    public static GameStageManager instance;

    private void Awake()
    {
        // Singleton
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public Stage gameStage = Stage.Menu;

    [Header("Container Scene Objects")]
    public GameObject menuObjects;
    public GameObject zenObjects;
    public GameObject workShopObjects;
    public GameObject kitchenObjects;
    public GameObject livingRoomObjects;

    [Header("Fog Transistion Settings")]
    public float fogFadeIn = 0.1f;
    public float fogFadeOut = 50f;
    public float fogFadeInTime = 2f;
    public float fogFadeOutTime = 2f;
    public float fogFadeHeldTime = 2f;
    public float timer = 0f;
    public bool fogging = false;
    public bool foggingInward = false;
    public bool delay = false;

    float perc;
    bool transition = true;

    private void Start()
    {
        RenderSettings.fogEndDistance = fogFadeOut;
        foggingInward = true;
        menuObjects.SetActive(true);
        zenObjects.SetActive(false);
        workShopObjects.SetActive(false);
        kitchenObjects.SetActive(false);
        livingRoomObjects.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) NextStage(); 

        if (fogging)
        {
            if (foggingInward)
            {
                RenderSettings.fogEndDistance = Mathf.Lerp(fogFadeOut, fogFadeIn, timer / fogFadeInTime);
                if (timer >= fogFadeInTime)
                {
                    timer = 0f;
                    fogging = false;
                    foggingInward = !foggingInward;
                    if (transition) Transition();
                }
            }
            else
            {
                RenderSettings.fogEndDistance = Mathf.Lerp(fogFadeIn, fogFadeOut, timer / fogFadeOutTime);
                if (timer >= fogFadeOutTime)
                {
                    timer = 0f;
                    fogging = false;
                    foggingInward = !foggingInward;
                    if (transition) Transition();
                }
            }
            timer += Time.deltaTime;
        }

        if (delay)
        {
            timer += Time.deltaTime;
            if (timer >= fogFadeHeldTime)
            {
                fogging = true;
                delay = false;
                timer = 0f;
            }
        }
    }

    public void NextStage()
    {
        fogging = true;
        transition = true;
    }

    void Transition()
    {
        transition = false;
        switch (gameStage)
        {
            case Stage.Menu: ZenStage();
                break;
            case Stage.Zen: WorkShopStage();
                break;
            case Stage.Workshop: KitchenStage();
                break;
            case Stage.Kitchen: LivingRoomStage();
                break;
            case Stage.LivingRoom: //do nothing and roll credits
                break;
            default:
                break;
        }
    }

    void ZenStage()
    {
        menuObjects.SetActive(false);
        zenObjects.SetActive(true);
        gameStage = Stage.Zen;
        delay = true;
    }

    void WorkShopStage()
    {
        zenObjects.SetActive(false);
        workShopObjects.SetActive(true);
        gameStage = Stage.Workshop;
        delay = true;
    }

    void KitchenStage()
    {
        workShopObjects.SetActive(false);
        kitchenObjects.SetActive(true);
        gameStage = Stage.Kitchen;
        delay = true;
    }

    void LivingRoomStage()
    {
        kitchenObjects.SetActive(false);
        livingRoomObjects.SetActive(true);
        gameStage = Stage.LivingRoom;
        delay = true;
    }

}
