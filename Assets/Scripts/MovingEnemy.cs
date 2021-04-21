using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public GameObject _enemy;
    public float moveSpeed;
    public Transform currentPosition;
    public Transform[] points;
    public int pointSelect;
    public int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = points[pointSelect];
    }

    // Update is called once per frame
    void Update()
    {
        _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, currentPosition.position,
            moveSpeed * Time.deltaTime);

        if (_enemy.transform.position == currentPosition.position)
        {
            pointSelect++;

            if (pointSelect == points.Length)
            {
                pointSelect = 0;
            }

            currentPosition = points[pointSelect];
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            health = health - 1;
            if (health == 0)
            {
                Destroy(gameObject);
            }
            /*Debug.Log(("Dash"));*/
        }
    }
}
