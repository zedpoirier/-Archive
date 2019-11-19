using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBatching : MonoBehaviour
{
    public static bool mainExists = false;
    private Mesh thisMesh;
    bool once = true;
    
    void Start() {
        thisMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertWorldPos = new Vector3[thisMesh.vertices.Length];
        for (int i = 0; i < vertWorldPos.Length; i++) {
            vertWorldPos[i] = transform.TransformPoint(thisMesh.vertices[i]);
        }
        if (mainExists) {
            
            GameObject.FindGameObjectWithTag("MainMesh").GetComponent<MeshBatching>().Batch(transform, thisMesh, vertWorldPos);
        }
        else {
            mainExists = true;
            gameObject.tag = "MainMesh";
            Batch(transform, thisMesh, vertWorldPos);
        }
    }

    //Batch
    void Batch(Transform pos, Mesh otherMesh, Vector3[] otherVertWorldPos) {
        Vector3[] theseVerts = thisMesh.vertices;
        int[] theseTris = thisMesh.triangles;
        if (pos.gameObject.tag == "MainMesh") {
            theseVerts = new Vector3[0];
            theseTris = new int[0];
        }

        Vector3[] newVerts = new Vector3[theseVerts.Length + otherVertWorldPos.Length];
        theseVerts.CopyTo(newVerts, 0);
        otherVertWorldPos.CopyTo(newVerts, theseVerts.Length);

        int[] otherTris = otherMesh.triangles;
        int[] newTris = new int[theseTris.Length + otherTris.Length];
        theseTris.CopyTo(newTris, 0);
        for (int t = 0; t < otherTris.Length; t++) {
            otherTris[t] += theseVerts.Length;
        }
        otherTris.CopyTo(newTris, theseTris.Length);

        thisMesh.vertices = newVerts;
        thisMesh.triangles = newTris;
        thisMesh.RecalculateBounds();
        thisMesh.RecalculateNormals();
        thisMesh.RecalculateTangents();

        if (pos.gameObject.tag != "MainMesh") {
            Destroy(pos.gameObject);
        }

        transform.position = transform.position - transform.position;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}
