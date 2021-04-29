using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveForce, maxSpeed, jumpForce;
    [SerializeField] private Collider2D groundCheck;
    /*[SerializeField] private Collider2D groundCheck_hero2;*/
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] public bool canDash;
    [SerializeField] public bool Dashing = false;
    
    public  static float moveDir;
    private Rigidbody2D myRB;
    private SpriteRenderer mySR;
    public float V_speed;
    public bool canJump;
    public Animator anim;
    
    /*public Sprite[] CookieLife;*/
    
    public CircleCollider2D myBox;
    public CircleCollider2D myBox2;
    /*public GameObject hero1;
    public GameObject hero2;*/
    
    //Health controls
    public static int currentHealth = 60 ;
    public HealthBar healthbar;
    
    //player evolution
    public  static bool p_level1;
    public static bool p_level2;
    
    //Dash Controls
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private float previousDirection = 1;
    
    public int p;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myBox = GetComponent<CircleCollider2D>();
        mySR = GetComponentInChildren<SpriteRenderer>();
        myBox2 = GetComponentInChildren<CircleCollider2D>();
        dashTime = startDashTime;   //Dash timer
        healthbar.SetMaxHealth(); //sets the max possible health in the HUD
        healthbar.SetHealth(currentHealth); //start the game with low health
        p_level1 = true;

    }


    private void FixedUpdate()
    {
        Debug.Log(myBox2.radius);
        evolutionCheck();
        var moveAxis = Vector2.right * moveDir;
        if (Mathf.Abs(myRB.velocity.x) < maxSpeed)
        {
            myRB.AddForce(moveAxis * moveForce, ForceMode2D.Force);
        }

        if (p_level1)
        {
            canJump = groundCheck.IsTouchingLayers(groundLayers);
        }
        else if (p_level2)
        {
            canJump = groundCheck.IsTouchingLayers(groundLayers);
            /*Debug.Log("hero 2 ground check"+ canJump);*/
        }
        if (myRB.velocity.x != 0)
        {
            previousDirection = myRB.velocity.x;
        }
        healthbar.SetHealth(currentHealth);
    }

    public void updateHealth()
    {
        currentHealth = currentHealth + 2;
    }
    public void updateHealth1()
    {
        currentHealth = currentHealth - 2;
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<float>();
        if (p_level1)
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
        else if(p_level2)
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
            if (p_level1)
            {
                p = 1;
            }
            else if (p_level2)
            {
                p = 2;
            }
            switch (p)
            {
                case 1:
                {
                    if (canJump)
                    {
                        myRB.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                        canJump = false;
                        anim.Play("Hero1_jump");   
                    }
                    else
                    {
                        anim.Play("Hero1_idle");
                    }

                    break;
                }
                case 2:
                {
                    if (canJump)
                    {
                        myRB.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                        canJump = false;
                        anim.Play("Hero2_jump");   
                    }
                    else
                    {
                        anim.Play("Hero2_Idle");
                    }

                    break;
                    
                }
            }
            /*if (p_level1)
            {
                myRB.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                canJump = false;
                anim.Play("Hero1_jump"); 
            }
            anim.Play("Hero1_idle");
            if (p_level2)
            {
                myRB.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                canJump = false;
                anim.Play("Hero2_jump");
            }
            anim.Play("Hero2_Idle");*/
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

   public void evolutionCheck()
   {
       if (currentHealth > 45)
       {
           p_level1 = false;
           p_level2 = true;
           myBox.radius = 1.1f;
           myBox2.radius = 1f;
        
           /*hero1.SetActive(false);
           hero2.SetActive(true);
           */

       }
       else if (currentHealth < 25)
       {
           p_level1 = true;
           p_level2 = false;
           myBox.radius = 0.5f;
           myBox2.radius = 0.5f;
           
           /*hero2.SetActive(false);
           hero1.SetActive(true);*/
       }
   }
}
