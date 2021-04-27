using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bot : MonoBehaviour
{
    public GameObject _enemyBot;

    public float moveSpeed;

    public Transform currentPosition;

    public Transform[] points;

    public int pointSelect;
    
    private SpriteRenderer mySR;
    // Start is called before the first frame update
    void Start()
    {
        mySR = GetComponentInChildren<SpriteRenderer>();
        currentPosition = points[pointSelect];
    }

    // Update is called once per frame
    void Update()
    {
        _enemyBot.transform.position = Vector3.MoveTowards(_enemyBot.transform.position, currentPosition.position,
            moveSpeed * Time.deltaTime);

        if (_enemyBot.transform.position == currentPosition.position)
        {
            pointSelect++;
            mySR.flipX = true;

            if (pointSelect == points.Length)
            {
                pointSelect = 0;
                mySR.flipX = false;
            }

            currentPosition = points[pointSelect];
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            /*Debug.Log(("Dash"));*/
        }
    }
}
