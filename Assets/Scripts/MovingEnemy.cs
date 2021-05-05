using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public GameObject _enemy;
    public GameObject _SJAbility;
    
    //enemy stats
    private float moveSpeed = 3;
    public  int health = 4;

    public Animator anim;
    //enemy patrolling control variables
    public Transform currentPosition;
    public Transform[] points;
    public int pointSelect;
    
    //invoke to check Player dash ability access
    public PlayerMovement Dash; 
    
    
    private SpriteRenderer mySR;    //Enemy sprite access
    //player movement script Dash ability call
    
    public GameObject roof ;
    
    //camera objects
    public CinemachineVirtualCamera MainCam;

    public CinemachineVirtualCamera Cutscene;
    // Start is called before the first frame update
    void Start()
    {
        mySR = GetComponentInChildren<SpriteRenderer>();
        currentPosition = points[pointSelect];
    }

    // Update is called once per frame
    void Update()
    {
        //patrolling enemy code that checks the patrol points assigned to it and flips sprite as per direction
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && Dash.Dashing == true)
        {
            Debug.Log(health);
            health = health - 1;
    
            if (health == 0)
            {
                playCutScene();//destroy enemy object
                Destroy(roof);
                _SJAbility.SetActive(true);
                
                //destroy the roof object in the map
            }
            /*Debug.Log(("Dash"));*/
        }
        
    }
    public void playCutScene()
    {
        Cutscene.enabled = true;
        
        MainCam.enabled = false;
        anim.Play("buff view");
        StartCoroutine(endCutScene());
    }
    IEnumerator endCutScene()
    {
        yield return new WaitForSeconds(6f);
        Debug.Log("end cut");
        MainCam.enabled = true;
        Cutscene.enabled = false;
        Destroy(gameObject); 
        

    }

   





}
