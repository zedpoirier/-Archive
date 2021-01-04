using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;
        if (GUILayout.Button("Seed Generation"))
        {
            mapGen.GenerateMap(mapGen.seed);
        }

        if (GUILayout.Button("Random Generation"))
        {
            mapGen.randomSeed = System.DateTime.Now.Millisecond.ToString();
            mapGen.GenerateMap(mapGen.randomSeed);
        }

        if (GUILayout.Button("Save Random Seed"))
        {
            mapGen.seed = mapGen.randomSeed;
        }

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap(mapGen.seed);
            }
        }

        
    }
}
