using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombination : MonoBehaviour
{
    [Tooltip("Name of gameObject containing the mesh.")]    public string targetName;
    [Tooltip("Shared material to be used.")]                public Material targetMaterial;
    [HideInInspector]
    [Tooltip("Collection of meshes to be combined.")]       public List<MeshFilter> meshes;
    
    // Combine
    public void Combine() {
        CombineInstance[] combines = new CombineInstance[meshes.Count];
        GameObject targetMesh = new GameObject(targetName);
        targetMesh.AddComponent<MeshFilter>();
        targetMesh.AddComponent<MeshRenderer>();

        for (int i = 0; i < meshes.Count; i++) {
            combines[i].mesh = meshes[i].sharedMesh;
            combines[i].transform = meshes[i].transform.localToWorldMatrix;
            //meshes[i].gameObject.SetActive(false);
            Destroy(meshes[i].gameObject);
        }

        targetMesh.GetComponent<MeshFilter>().mesh = new Mesh();
        targetMesh.GetComponent<MeshFilter>().mesh.CombineMeshes(combines);
        targetMesh.GetComponent<MeshRenderer>().material = targetMaterial;
        targetMesh.transform.SetParent(transform);
    }
}
