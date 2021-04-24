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
    
    //Health controls
    public int maxHealth = 100;
    public int currentHealth = 10;
    public HealthBar healthbar;
    
    //Dash Controls
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private float previousDirection = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(currentHealth);

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

        if (myRB.velocity.x != 0)
        {
            previousDirection = myRB.velocity.x;
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
            if (canDash && !Dashing)
            {
                if (dashTime <= 0)
                {
                    dashTime = startDashTime;
                    myRB.velocity = Vector2.zero;
                    Dashing = false;

                }
                else
                {
                    Dashing = false;
                    dashTime -= Time.deltaTime;
                    if (myRB.velocity.x < 0)
                    {
                        myRB.velocity = Vector2.left * dashSpeed;
                        Dashing = true;
                        Invoke("Dashtimer", 1.0f);
                    }
                    else if (myRB.velocity.x > 0)
                    {
                        myRB.velocity = Vector2.right * dashSpeed;
                        Dashing = true;
                        Invoke("Dashtimer", 1.0f);
                    }
                    else
                    {
                        if(previousDirection < 0) myRB.velocity = Vector2.left * dashSpeed;
                        if(previousDirection > 0) myRB.velocity = Vector2.right * dashSpeed;
                       
                        Dashing = true;
                        Invoke("Dashtimer", 1.0f);
                    }
                }
            }

        }
    
        
    }
   void Dashtimer()
   {
       Dashing = false;
   }

}
