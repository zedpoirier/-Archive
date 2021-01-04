using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerType { Input, Audio, Raw, Horizontal, Generator }
public enum LineColor { Blue, Green, Red, Purple }

public class LineController : MonoBehaviour
{
    public ControllerType type;
    public Transform leadIcon;
    public LineRenderer lineRend;
    public Vector3 pos;
    public Vector3 lastPoint;
    public float yPos;
    public float xTime;
    public int pointIndex;
    public float timer;

    [Header("Type: Input || Generated")]
    public LineColor color;
    public Vector2 jumpRangeA; // min and max
    public Vector2 timeRange; // min adn max
    public float timeBeforeNextJump; // units per second
    public float timeMod; // units per second
    public bool triggered;

    [Header("Type: Audio || Raw")]
    public AudioAnalyzer AA;
    public Vector2 jumpRangeB;
    public float stepTime;
    public bool doubleBand;
    public int bandRef;


    private void Start()
    {
        if (type == ControllerType.Generator)
        {
            triggered = true;
            timeBeforeNextJump = Random.Range(timeRange.x, timeRange.y);
        }
        yPos = Random.Range(jumpRangeB.x, jumpRangeB.y);
        if (type == ControllerType.Input) yPos = Random.Range(jumpRangeA.x, jumpRangeA.y);
        lineRend = GetComponent<LineRenderer>();
        lineRend.SetPosition(0, new Vector3(0, yPos, 0));
        lineRend.SetPosition(1, new Vector3(0, yPos, 0));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { type = ControllerType.Input; }// swap to jumping keep lines connected


        pointIndex = lineRend.positionCount - 1;
        lastPoint = lineRend.GetPosition(pointIndex);
        xTime = Time.time * timeMod;

        pos = new Vector3(xTime, lastPoint.y, 0);
        lineRend.SetPosition(pointIndex, pos);
        
        switch (type)
        {
            case ControllerType.Input: RandomJump();
                break;
            case ControllerType.Audio: MatchAudioBands();
                break;
            case ControllerType.Raw: MatchRawBands();
                break;
            case ControllerType.Horizontal: HorizontalJumps();
                break;
            case ControllerType.Generator: GenerateLines();
                break;
            default:
                break; 
        }

        if (leadIcon != null) leadIcon.position = lastPoint;
    }

    void LineJump()
    {
        lineRend.positionCount++;
        lineRend.SetPosition(pointIndex + 1, pos);
        lineRend.positionCount++;
        lineRend.SetPosition(pointIndex + 2, pos);
    }

    void RandomJump() // Trigger specific lines to jump using the nums 1 to 4
    {
        switch (color)
        {
            case LineColor.Blue: if (Input.GetKeyDown(KeyCode.Alpha1)) triggered = true;
                break;
            case LineColor.Green: if (Input.GetKeyDown(KeyCode.Alpha2)) triggered = true;
                break;
            case LineColor.Red: if (Input.GetKeyDown(KeyCode.Alpha3)) triggered = true;
                break;
            case LineColor.Purple: if (Input.GetKeyDown(KeyCode.Alpha4)) triggered = true;
                break;
            default:
                break;
        }
        if (triggered)
        {
            triggered = false;
            yPos = Random.Range(jumpRangeA.x, jumpRangeA.y);
            pos = new Vector3(xTime, yPos, 0);
            LineJump();
        }
        
    }

    void MatchAudioBands() // Lines jump at set intervals based on the audiobands of the music
    {
        if (timer > stepTime)
        {
            yPos = Mathf.Lerp(jumpRangeB.x, jumpRangeB.y, AA.audioBand[bandRef]);
            if (doubleBand)
            {
                float combinedBands = (AA.audioBand[bandRef] + AA.audioBand[bandRef + 1]) / 2f;
                yPos = Mathf.Lerp(jumpRangeB.x, jumpRangeB.y, combinedBands);
            }
            pos = new Vector3(xTime, yPos, 0);
            LineJump();
            timer = 0f;
        }
        timer += Time.deltaTime;
    }

    void MatchRawBands() // Lines jump at set intervals based on frequency bands + and offset
    {
        if (timer > stepTime)
        {
            yPos = AA.freqBands[bandRef] + jumpRangeB.x;
            pos = new Vector3(xTime, yPos, 0);
            LineJump();
            timer = 0f;
        }
        timer += Time.deltaTime;
    }

    void HorizontalJumps() // Flips the jumping to make horizontal bars, based on audio bands
    {
        if (timer > stepTime)
        {
            yPos = Mathf.Lerp(jumpRangeB.x, jumpRangeB.y, AA.audioBand[bandRef]);
            if (doubleBand)
            {
                float combinedBands = (AA.audioBand[bandRef] + AA.audioBand[bandRef + 1]) / 2f;
                yPos = Mathf.Lerp(jumpRangeB.x, jumpRangeB.y, combinedBands);
            }
            pos = new Vector3(xTime, yPos, 0);
            Vector3 pos2 = new Vector3(0, yPos, 0);
            lineRend.positionCount++;
            lineRend.SetPosition(pointIndex + 1, pos2);
            lineRend.positionCount++;
            lineRend.SetPosition(pointIndex + 2, pos);
            timer = 0f;
        }
        timer += Time.deltaTime;
    }

    void GenerateLines() //  Randomize the time between jumps and automate everything
    {
        timer += Time.deltaTime;
        if (timer >= timeBeforeNextJump)
        {
            timer = 0f;
            timeBeforeNextJump = Random.Range(timeRange.x, timeRange.y);
            yPos = Random.Range(jumpRangeA.x, jumpRangeA.y);
            pos = new Vector3(xTime, yPos, 0);
            LineJump();
        }
    }
}
