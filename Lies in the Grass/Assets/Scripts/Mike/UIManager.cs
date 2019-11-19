using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public static UIManager Instance;

    private TextMeshProUGUI escapesText;
    private TextMeshProUGUI deathsText;

    private float escapes;
    private float deaths;

    private void Start() {
        if (Instance) {
            Debug.LogWarning("UIManager already exists, deleting duplicate.");
            Destroy(gameObject);
        }

        Instance = this;
        escapesText = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        deathsText = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

        escapes = 0;
        deaths = 0;
    }

    public void IncrementEscapes() {
        escapes += 1;
        escapesText.text = "Escapes: " + escapes.ToString("0000");
    }

    public void IncrementDeaths() {
        deaths += 1;
        deathsText.text = "Deaths: " + deaths.ToString("0000");
    }
}
