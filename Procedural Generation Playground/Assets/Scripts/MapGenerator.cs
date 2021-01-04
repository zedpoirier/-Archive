using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode{ NoiseMap, ColorMap, Mesh };
    public DrawMode drawMode;
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public bool autoUpdate;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public int octaves;
    [Range(0f,1f)]
    public float persistance;
    public float lacunarity;
    public Vector2 offset;
    [Range(0f,1f)]
    public float borderFalloff;
    public float fallOffAmount = 4f;
    public string seed;

    public TerrainType[] regions;

    public string randomSeed;

    public void GenerateMap(string seed)
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale, 
            octaves, persistance, lacunarity, offset, seed);

        Color[] colorMap = new Color[mapWidth * mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];

                float xEdgeDistance = (float)x / (mapWidth / 2f) - 1f; // close to 0 is the edge
                float yEdgeDistance = (float)y / (mapHeight / 2f) - 1f; // close to 0 is the edge
                Vector2 point = new Vector2(xEdgeDistance, yEdgeDistance);
                //Debug.Log(xEdgeDistance);

                currentHeight -= (point.magnitude * fallOffAmount) * borderFalloff;

                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        if (drawMode == DrawMode.NoiseMap) 
        { 
            DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap)); 
        }
        else if (drawMode == DrawMode.ColorMap) 
        { 
            DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight)); 
        }
        else if (drawMode == DrawMode.Mesh)
        {
            DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap),
                TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }

    }

    public void DrawTexture(Texture2D texture)
    {
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
        meshRenderer.sharedMaterial.SetTexture("_EmissionMap", texture);
    }

    private void OnValidate()
    {
        if (mapWidth < 1) mapWidth = 1;
        if (mapHeight < 1) mapHeight = 1;
        if (lacunarity < 1) lacunarity = 1;
        if (octaves < 0) octaves = 0;
    }

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }
}
