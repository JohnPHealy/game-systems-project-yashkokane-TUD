using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveForce, maxSpeed, jumpForce;
    [SerializeField] private Collider2D groundCheck;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] public bool canDash;
    [SerializeField] public bool Dashing = false;
    
    private float moveDir;
    private Rigidbody2D myRB;
    private bool canJump;
    
    //Dash Controls
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
       
    }


    private void FixedUpdate()
    {
        var moveAxis = Vector2.right * moveDir;
        
        if (Mathf.Abs(myRB.velocity.x) < maxSpeed)
        {
            myRB.AddForce(moveAxis * moveForce, ForceMode2D.Force);

        }
        if (groundCheck.IsTouchingLayers(groundLayers))
        {
            canJump = true;

        }
        else
        {
            canJump = false;

            
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<float>();
        
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (canJump && context.started)
        {
            myRB.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            
            canJump = false;
        }

        if (context.canceled && myRB.velocity.y > 0)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, 0f);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(canDash && !Dashing)
                {
                    if(direction == 0)
                    {
                        if(moveDir < 0)
                        {
                            direction = 1;
                        }
                        else if (moveDir > 0)
                        {
                            direction = 2;
                        }
                    }
                    else
                    {
                        if(dashTime <= 0)
                        {
                            direction = 0;
                            dashTime = startDashTime;
                            myRB.velocity = Vector2.zero;
                            Dashing = false;
            
                        }
                        else
                        {
                            Dashing = false;
                            dashTime -= Time.deltaTime;
                            if(direction == 1)
                            {
                                myRB.velocity = Vector2.left * dashSpeed;
                                Dashing = true;
                                /*Debug.Log("Dashing left");*/
                                direction = 0;
                                Invoke("Dashtimer", 1.0f);
                            }else if (direction == 2)
                            {
                                myRB.velocity = Vector2.right * dashSpeed;
                                Dashing = true;
                                /*Debug.Log("Dashing right");*/
                                direction = 0;
                                Invoke("Dashtimer", 1.0f);
                            }
                        }
                    }
                }
        }
        /*Debug.Log("is dashing");*/
    
        
    }
   void Dashtimer()
   {
       Dashing = false;
   }

}
