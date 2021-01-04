using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRCenterPlayArea : MonoBehaviour
{
    public XRController controller;
    public Transform VRCamera;
    public bool resetStage;
    public bool triggerValue;
    public bool isTriggered;

    void Update()
    {
        if(controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            isTriggered = true;
        }

        if (isTriggered && !triggerValue)
        {
            Debug.Log("REcenter Area");
            transform.position = new Vector3(-VRCamera.localPosition.x, 0f, -VRCamera.localPosition.z);
            isTriggered = false;
            if (resetStage) ResetCurrentStage();
        }
    }

    void ResetCurrentStage()
    {
        //GameStageManager.instance.gameStage;
    }
}
