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
    private SpriteRenderer mySR;
    public float V_speed;
    private bool canJump;
    public Animator anim;
    
    //Health controls
    public int maxHealth = 100;
    public int currentHealth ;
    public HealthBar healthbar;
    
    
    //Dash Controls
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private float previousDirection = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 10;
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponentInChildren<SpriteRenderer>();
        dashTime = startDashTime;   //Dash timer
        healthbar.SetMaxHealth(); //sets the max possible health in the HUD
        healthbar.SetHealth(currentHealth); //start the game with low health

    }


    private void FixedUpdate()
    {
        
        var moveAxis = Vector2.right * moveDir;
        if (moveDir > 0)
        {
            mySR.flipX = false;
        }
        if (moveDir < 0)
        {
            mySR.flipX = true;
        }
        
        if (Mathf.Abs(myRB.velocity.x) < maxSpeed)
        {
            myRB.AddForce(moveAxis * moveForce, ForceMode2D.Force);

        }
        if (groundCheck.IsTouchingLayers(groundLayers))
        {
            canJump = true;
            anim.SetBool("isGrounded", true);

        }
        else
        {
            canJump = false;
            anim.SetBool("isGrounded", false);
            
        }

        if (myRB.velocity.x != 0)
        {
            previousDirection = myRB.velocity.x;
        }
        healthbar.SetHealth(currentHealth);
        
        
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<float>();
        anim.SetFloat("isMoving", moveDir);

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
                        anim.SetBool("isDashing",true);
                        Invoke("Dashtimer", 1.0f);
                    }
                    else if (myRB.velocity.x > 0)
                    {
                        myRB.velocity = Vector2.right * dashSpeed;
                        Dashing = true;
                        anim.SetBool("isDashing",true);
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
       anim.SetBool("isDashing",false);
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
       if (other.gameObject.tag == "Enemy")
       {
           currentHealth = currentHealth - 1;
       }
   }
}
