using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerObject : MonoBehaviour
{
    public UnityEvent triggered;

    private void OnCollisionEnter(Collision collision)
    {
        triggered.Invoke();
        //GameStageManager.instance.ToZenStage();
    }
}
