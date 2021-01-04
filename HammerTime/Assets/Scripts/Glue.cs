using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Clayxels;

public class Glue : MonoBehaviour
{
    public Animator glueSpray;
    public GameNails gameNails;
    public bool sprayed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer" && !sprayed)
        {
            glueSpray.gameObject.SetActive(true);
            glueSpray.SetTrigger("Spray");
            sprayed = true;
            gameNails.StartGame();
        }
    }
}
