using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaypointList : MonoBehaviour {
    private static Transform[] allPoints;
    private static int innerEnd;

    public static Vector3[] Allpoints {
        get {
            Vector3[] points = new Vector3[allPoints.Length];
            for (int i = 0; i <= allPoints.Length; i++) {
                points[i] = allPoints[i].position;
            }
            return points;
        }
    }

    public static Vector3[] InnerPoints {
        get {
            Vector3[] innerPoints = new Vector3[innerEnd + 1];
            for (int i = 0; i <= innerEnd; i++) {
                innerPoints[i] = allPoints[i].position;
            }
            return innerPoints;
        }
    }

    public static Vector3[] OuterPoints {
        get {
            Vector3[] outerPoints = new Vector3[allPoints.Length - (innerEnd + 1)];
            for (int i = innerEnd + 1; i < allPoints.Length; i++) {
                outerPoints[i - (innerEnd + 1)] = allPoints[i].position;
            }
            return outerPoints;
        }
    }

    public static Vector3 RandomPosition {
        get {
            int rand = Random.Range(0, allPoints.Length - 1);
            return allPoints[rand].position;
        }
    }

    public static Vector3 RandomInnerPosition {
        get {
            return allPoints[Random.Range(0, innerEnd)].position;
        }
    }

    public static Vector3 RandomOuterPosition {
        get {
            return allPoints[Random.Range(innerEnd + 1, allPoints.Length - 1)].position;
        }
    }

    // Start is called before the first frame update
    private void Awake() {
        Transform inner = GameObject.Find("Inner Points").transform;
        Transform outer = GameObject.Find("Outer Points").transform;

        innerEnd = inner.transform.childCount - 1;
        allPoints = new Transform[inner.childCount + outer.childCount];

        for (int i = 0; i < allPoints.Length; i++) {
            if (i <= innerEnd) {
                allPoints[i] = inner.GetChild(i);
            } else {
                allPoints[i] = outer.GetChild(i - innerEnd - 1);
            }
        }
    }
}
