using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour {

    [Header("Spawn Settings")]
    public ParticleSystem hearts;
    public int spaceHeight = 10;
    public int spaceWidth = 10;

    [Header("Manipulation Settings")]
    public List<ParticleSystem> ps;
    public float originalHideRange;
    public float hideRangeIncrement = 2;
    public float smallAreaHideRange = 5f;
    public float largeAreaHideRange = 10f;

	// Use this for initialization
	void Start () {
        Spawn();
        originalHideRange = ps[0].GetComponent<Hearts>().hideRange;
	}
	

    void Spawn()
    {
        for (int x = 0; x < spaceWidth; x++)
        {
            for (int y = 0; y < spaceHeight; y++)
            {
                Vector3 pos = new Vector3(x - (spaceWidth / 2), y - (spaceHeight / 2), -6);
                if (pos.x == 1 && pos.y == 1 || 
                    pos.x == 1 && pos.y == 0 || 
                    pos.x == 1 && pos.y == -1 || 
                    pos.x == 0 && pos.y == 1 || 
                    pos.x == 0 && pos.y == 0 || 
                    pos.x == 0 && pos.y == -1 || 
                    pos.x == -1 && pos.y == 1 || 
                    pos.x == -1 && pos.y == 0 || 
                    pos.x == -1 && pos.y == -1)
                {
                    //Do nothing
                }
                else
                {
                    ParticleSystem temp = Instantiate(hearts, pos, Quaternion.identity);
                    ps.Add(temp);
                }
            }
        }
    }

    public void HideSmallArea()
    {
        for (int i = 0; i < ps.Count; i++)
        {
            ps[i].GetComponent<Hearts>().hideRange = smallAreaHideRange;
        }
    }

    public void RevertHideArea()
    {
        for (int i = 0; i < ps.Count; i++)
        {
            ps[i].GetComponent<Hearts>().hideRange = originalHideRange;
        }
    }

    public void IncreaseHideRange(int increaseCount)
    {
        for (int i = 0; i < ps.Count; i++)
        {
            ps[i].GetComponent<Hearts>().hideRange = originalHideRange + (increaseCount * hideRangeIncrement);
        }
    }
}
