using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : MonoBehaviour {

    public bool withinRange = false;
    public bool isTalking = false;
    public Zoom zoomer;
    public PlayerMovement mover;
    public Dialogue target;
    public HeartController hearts;
    public int talkCounter = 0;
    public CreditAnim[] credits;

    Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();
        mover = GetComponent<PlayerMovement>();
        hearts = GameObject.FindGameObjectWithTag("Hearts").GetComponent<HeartController>();
	}

    void EngageConversation()
    {
        mover.Stop();
        isTalking = true;
        target.Call();
        zoomer.ZoomOnConvo();
    }

    public void DisengageConversation()
    {
        isTalking = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.GetComponent<Dialogue>())
        {
            target = col.gameObject.GetComponent<Dialogue>();
            withinRange = true;
            zoomer.person = col.transform;
            zoomer.personPos = col.transform.position;
            talkCounter++;
            hearts.IncreaseHideRange(talkCounter);
            if (withinRange && !isTalking)
            {
                EngageConversation();
            }
            Destroy(col.gameObject.GetComponent<Collider2D>());
            withinRange = false;
            zoomer.person = null;
        }
        else if (col.gameObject.GetComponent<AppearAfterComplete>())
        {
            zoomer.timer = 0.0f;
            mover.Center();
            anim.SetTrigger("Win");
            StartCoroutine("WidenHideArea");
            zoomer.zoomed = true;
            zoomer.zooming = false;
            zoomer.winning = true;
            col.gameObject.GetComponent<AppearAfterComplete>().Found();
        }
    }

    public IEnumerator WidenHideArea()
    {
        while (talkCounter < 100)
        {
            yield return new WaitForSeconds(0.1f);
            talkCounter++;
            hearts.IncreaseHideRange(talkCounter);
        }
        for (int i = 0; i < credits.Length; i++)
        {
            credits[i].revealing = true;
        }
    }
}
