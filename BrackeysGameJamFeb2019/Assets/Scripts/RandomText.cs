using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomText : MonoBehaviour {

    private string all = "abcdefghijklmnopqrstuvwxyz";
    private string generatedString;

    public string GenerateText(int wordCount, int minchar, int maxChar)
    {
        generatedString = "";
        for (int i = 0; i < wordCount; i++)
        {
            int charCount = Random.Range(minchar, maxChar);
            for (int c = 0; c < charCount; c++)
            {
                generatedString += all[Random.Range(0, 26)];
            }
            generatedString += " ";
        }
        //generatedString[0] = char.ToUpper(generatedString[0]);
        return generatedString;
    }

    
}
