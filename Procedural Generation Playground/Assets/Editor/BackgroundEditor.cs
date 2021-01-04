using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BackgroundGenerator)), CanEditMultipleObjects]
public class BackgroundEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BackgroundGenerator backgroundGen = (BackgroundGenerator)target;
        if(GUILayout.Button("Seed Generation"))
        {
            for (int t = 0; t < targets.Length; t++)
            {
                BackgroundGenerator backGen = (BackgroundGenerator)targets[t];
                backGen.GenerateBackground(backGen.seed);
            }
        }

        if (GUILayout.Button("Random Generation"))
        {
            for (int t = 0; t < targets.Length; t++)
            {
                BackgroundGenerator backGen = (BackgroundGenerator)targets[t];
                backGen.randomSeed = System.DateTime.Now.ToString() 
                    + System.DateTime.Now.Millisecond.ToString()
                    + t.ToString();
                backGen.GenerateBackground(backGen.randomSeed);
            }
        }

        if (GUILayout.Button("Save Current Random Seed"))
        {
            for (int t = 0; t < targets.Length; t++)
            {
                BackgroundGenerator backGen = (BackgroundGenerator)targets[t];
                backGen.seed = backGen.randomSeed;
            }
        }

        if (DrawDefaultInspector())
        {
            if (backgroundGen.autoUpdate)
            {
                backgroundGen.GenerateBackground(backgroundGen.seed);
            }
        }
    }
}
