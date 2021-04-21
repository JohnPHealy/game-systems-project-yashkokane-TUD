using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_floor : MonoBehaviour
{
    public PlayerMovement Dash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Dash.Dashing == true)
            {
                Destroy(gameObject);
            }
        }
    }
}