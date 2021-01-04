using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveGenerator : MonoBehaviour
{
    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int fillPercentage;

    int[,] map;

    void Start()
    {
        GenerateCave();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { GenerateCave(); }
        if (Input.GetKeyDown(KeyCode.Return)) { SmoothMap(); }
    }

    void GenerateCave()
    {
        if (useRandomSeed) { seed = System.DateTime.Now.ToString(); }
        map = new int[width, height];
        System.Random random = new System.Random(seed.GetHashCode());
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int result = 0;
                if (random.Next(0,100) < fillPercentage) { result = 1; }
                map[x, y] = result;
            }
        }
    }

    // Used to check the surrounding wall tiles and alter the current tile
    // to match it's neighbours.
    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallCount = GetSurroundingWallCount(x, y);
                if (neighbourWallCount > 4) { map[x, y] = 1; }
                else { map[x, y] = 0; }
            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0; 
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY -1 ; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    wallCount += map[neighbourX, neighbourY];
                }
                else {
                    wallCount += 1; // Treating the exterior of the map as a wall.
                }
            }
        }
        return wallCount;
    }

    void OnDrawGizmos()
    {
        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x, y] == 1) { Gizmos.color = Color.black; }
                    else { Gizmos.color = Color.white; }
                    Vector3 pos = new Vector3(
                        -width/2 + x + 0.5f,
                        -height/2 + y + 0.5f,
                        0);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }
}
