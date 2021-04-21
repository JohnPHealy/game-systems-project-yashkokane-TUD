using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_Ability : MonoBehaviour
{
    public PlayerMovement Dash;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dash.canDash = true;
            /*Debug.Log(("Dash"));*/
        }
    }
}
