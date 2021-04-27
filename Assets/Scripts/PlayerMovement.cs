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
    
    public  static float moveDir;
    private Rigidbody2D myRB;
    private SpriteRenderer mySR;
    public float V_speed;
    private bool canJump;
    public Animator anim;
    
    //Health controls
    public int maxHealth = 100;
    public int currentHealth ;
    public HealthBar healthbar;
    
    //player evolution
    public bool p_level1;
    public bool p_level2;
    
    
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
        p_level1 = true;

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
        healthbar.SetHealth(currentHealth);
        if (currentHealth == 30)
        {
            p_level1 = false;
            anim.SetBool("P_level1" , false);
            p_level2 = true;
            anim.SetBool("P_level2", true);
        }
        
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<float>();
        if (p_level1 == true )
        {
            if (moveDir >0)
            {
                mySR.flipX = false;
                anim.Play("Hero1_run");
            }
            else if (moveDir < 0)
            {
                mySR.flipX = true;
                anim.Play("Hero1_run");
            }
            else
            {
                anim.Play("Hero1_idle");
            }
        }
        else if(p_level2 == true)
        {
            if (moveDir >0)
            {
                mySR.flipX = false;
                anim.Play("Hero2_run");
            }
            else if (moveDir < 0)
            {
                mySR.flipX = true;
                anim.Play("Hero2_run");
            }
            else
            {
                anim.Play("Hero2_Idle");
            }
        }
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (canJump && context.started)
        {
            myRB.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            anim.Play("Hero1_jump");
        }
        else
        {
            anim.Play("Hero1_idle");
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
                        if (p_level1 == true)
                        {
                            anim.Play("Hero1_Dash");
                            Invoke("Dashtimer", 1.0f); 
                        }
                        else if (p_level2 == true)
                        {
                            anim.Play("Hero2_Dash");
                            Invoke("Dashtimer", 1.0f); 
                        }
                        
                    }
                    else if (myRB.velocity.x > 0)
                    {
                        myRB.velocity = Vector2.right * dashSpeed;
                        Dashing = true;
                        if (p_level1 == true)
                        {
                            anim.Play("Hero1_Dash");
                            Invoke("Dashtimer", 1.0f); 
                        }
                        else if (p_level2 == true)
                        {
                            anim.Play("Hero2_Dash");
                            Invoke("Dashtimer", 1.0f); 
                        }
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
