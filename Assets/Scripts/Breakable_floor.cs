using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_floor : MonoBehaviour
{
    public PlayerMovement Dash;

    private ParticleSystem particles;

    private SpriteRenderer mySR;

    private BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        mySR = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
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
                StartCoroutine(Break());
            }
        }
    }
    IEnumerator Break()
    {
        particles.Play();

        mySR.enabled = false;
        bc.enabled = false;
        yield return new WaitForSeconds(particles.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}


