using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public bool autoUpdate;
    public string seed;
    public string randomSeed;
    //public int layerCount;
    public int quadCount;
    public MeshFilter filter;
    public MeshRenderer rendy;


    public Vector2 verticalJumpRange;
    public Vector2 horizontalJumpRange;

    public Vector3 pos;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public void GenerateBackground(string seed)
    {
        Random.InitState(seed.GetHashCode());
        mesh = new Mesh();
        mesh.vertices = GeneratePoints();
        mesh.triangles = GenerateTriangles();
        //mesh.SetVertices(GeneratePoints()); // faster than ToArray conversion
        //mesh.SetTriangles(GenerateTriangles(), 0); // ditto
        mesh.RecalculateNormals();
        filter.mesh = mesh;
        
    }

    public Vector3[] GeneratePoints() 
    {
        List<Vector3> points = new List<Vector3>();
        float x = 0;
        float y;

        for (int i = 0; i < quadCount; i++)
        {
            y = Random.Range(verticalJumpRange.x, verticalJumpRange.y);
            points.Add(new Vector3(x, y, 0));
            points.Add(new Vector3(x, 0, 0));
            x += Random.Range(horizontalJumpRange.x, horizontalJumpRange.y);
            points.Add(new Vector3(x, y, 0));
            points.Add(new Vector3(x, 0, 0));
        }
        vertices = points.ToArray();
        return points.ToArray();
    }

    public int[] GenerateTriangles()
    {
        List<int> triangleIndexes = new List<int>();

        for (int t = 0; t < vertices.Length; t+=4)
        {
            triangleIndexes.Add(t);
            triangleIndexes.Add(t + 3);
            triangleIndexes.Add(t + 1);
            triangleIndexes.Add(t);
            triangleIndexes.Add(t + 2);
            triangleIndexes.Add(t + 3);
        }
        triangles = triangleIndexes.ToArray();
        return triangleIndexes.ToArray();
    }


}
