// Code: Zed Poirier
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Generate an array of vertex positions and another array of distances from those 
/// vertices to a specified reference point, or the origin of the gameobject.
/// </summary>
[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class VertexSpawner : MonoBehaviour {

    [Header("Spawning Properties")]
    [Tooltip("Min prefabs per vertex")][Range(1, 9)]    public int minPrefabPerVert;
    [Tooltip("Max prefabs per vertex")][Range(1, 10)]   public int maxPrefabPerVert;
    [Tooltip("Cutoff distance of spawnpoints.")]        public float cutOffDistance;
    [Tooltip("Min and max radius for prefab pos")]      public Vector2 spawnRadius;
    [Tooltip("Alternate point for vertex distance.")]   public Transform referencePoint;
    [Tooltip("Prefab object to spawn.")]                public GameObject prefab;

    private Mesh mesh;                  // Mesh.
    private float[] vertexDis;          // Vertex distances from the reference point.
    private Vector3[] vertexPos;        // Vertex positions in world space.
    private List<Vector3> spawnPos;     // Spawn points to be altered for style.
    private MeshCombination combiner;   // Reference the MeshCombination component.

    void Start() { // Starting setup.
        combiner = GetComponent<MeshCombination>();
        if (GetComponent<MeshFilter>().mesh.vertexCount <= 0) { // Check mesh.
            Debug.LogError("You need mesh data!", this);
        } 
        else {
            mesh = GetComponent<MeshFilter>().mesh;
        }
        if (referencePoint == null) { // Check reference point.
            referencePoint = transform;
        }
        GeneratePositions();
        GenerateDistances();
        SpawnPrefabs();
    }

    void GeneratePositions() { // 
        vertexPos = new Vector3[mesh.vertexCount];
        for (int i = 0; i < vertexPos.Length; i++) {
            vertexPos[i] = transform.TransformPoint(mesh.vertices[i]);
        }
    }

    void GenerateDistances() {
        vertexDis = new float[vertexPos.Length];
        for (int i = 0; i < vertexDis.Length; i++) {
            vertexDis[i] = Vector3.Distance(vertexPos[i], referencePoint.position);
        }
    }

    void SpawnPrefabs() {
        spawnPos = vertexPos.Distinct().ToList();
        for (int i = 0; i < spawnPos.Count; i++) {
            float distance = Mathf.Abs(spawnPos[i].y - referencePoint.position.y);
            if (distance >= cutOffDistance) {
                int num = Random.Range(minPrefabPerVert, maxPrefabPerVert);
                for (int n = 0; n < num; n++) {
                    Vector3 offset = Random.insideUnitSphere * Random.Range(spawnRadius.x, spawnRadius.y);
                    Vector3 spawnPoint = spawnPos[i] + offset;
                    GameObject temp = Instantiate(prefab, spawnPoint, Random.rotation, transform);
                    combiner.meshes.Add(temp.GetComponent<MeshFilter>());
                }
            }
        }
        combiner.Combine();
    }
}
