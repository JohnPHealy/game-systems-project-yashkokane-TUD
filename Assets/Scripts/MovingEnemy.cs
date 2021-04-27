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
    
    private SpriteRenderer mySR;
    //player movement script Dash ability call
    
    public PlayerMovement Dash;
    // Start is called before the first frame update
    void Start()
    {
        mySR = GetComponentInChildren<SpriteRenderer>();
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
            
            mySR.flipX = true;

            if (pointSelect == points.Length)
            {
                pointSelect = 0;
                
                mySR.flipX = false;
            }

            currentPosition = points[pointSelect];
        }
    }
    
    private void OnCollisionEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Dash.Dashing == true)
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
