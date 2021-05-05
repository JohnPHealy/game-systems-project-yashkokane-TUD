using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveForce, maxSpeed, jumpForce;
    [SerializeField] private Collider2D groundCheck;
    [SerializeField] private Collider2D groundCheck_hero2;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] public bool canDash;
    [SerializeField] public bool Dashing = false;
    public gameManager _GM;
    
    [SerializeField] public bool canSJ;
    public  static float moveDir;
    private Rigidbody2D myRB;
    private SpriteRenderer mySR;
    public float V_speed;
    public bool canJump;
    public Animator anim;
    
    /*public Sprite[] CookieLife;*/
    
    public CircleCollider2D myCir;

    public BoxCollider2D myBox;
    /*public GameObject hero1;
    public GameObject hero2;*/
    
    //Health controls
    public static int PlayerHealth = 12 ;
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
    
    //super jump controls
    public bool _superJump;
    public float chargeUp;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myCir = GetComponent<CircleCollider2D>();
        myBox = GetComponentInChildren<BoxCollider2D>();
        mySR = GetComponentInChildren<SpriteRenderer>();
        dashTime = startDashTime;   //Dash timer
        healthbar.SetMaxHealth(); //sets the max possible health in the HUD
        healthbar.SetHealth(PlayerHealth); //start the game with low health
        p_level1 = true;

    }


    private void FixedUpdate()
    {
        evolutionCheck(); //to check if the player has reached the evolution stage
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
        }
        if (myRB.velocity.x != 0)
        {
            previousDirection = myRB.velocity.x;
        }
        healthbar.SetHealth(PlayerHealth); //passes health to the health bar script
        healtCheck(); 
        
    }

    public void updateHealth()
    {
        PlayerHealth = PlayerHealth + 1;
       
    }
    public void updateHealth1()
    {
        PlayerHealth = PlayerHealth - 3;
        
    }

    public void healtCheck()
    {
        if (PlayerHealth == 0)
        {
            /*Debug.Log("health zero");*/
            SceneManager.LoadScene("game_over");
        }
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
                        myRB.AddForce(transform.up * jumpForce * 1.2f, ForceMode2D.Impulse);
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
        else
        {
            if (p_level1 == true)
            {
                anim.Play("Hero1_idle");
            }
            else if (p_level2 == true)
            {
                anim.Play("Hero2_Idle");
            }
        }
    }
   void Dashtimer()
   {
       Dashing = false;
       anim.SetBool("isDashing",false);
   }

   IEnumerator  jumphalt()
   {
       yield return new WaitForSeconds(1.2f); 
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
                   myRB.AddForce(transform.up * jumpForce * chargeUp , ForceMode2D.Impulse );
                   canJump = false;
                   anim.Play("Hero1_jump");
                   chargeUp = 0f;
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
                   myRB.AddForce(transform.up * jumpForce*chargeUp, ForceMode2D.Impulse);
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
       /*
       anim.SetBool("isDashing",false);*/
   }
   public void SuperJump(InputAction.CallbackContext context)
   {
       if (context.started && canSJ )
       {
           chargeUp = jumpForce * 0.08f;
           StartCoroutine(jumphalt());
           Debug.Log(chargeUp);
           
       }
       
   }
    private void OnCollisionEnter2D(Collision2D other)
   {
       if (other.gameObject.tag == "Enemy")
       {
           PlayerHealth = PlayerHealth - 1;
       }
   }

   public void evolutionCheck()
   {
       if (PlayerHealth == 50)
       {
           p_level1 = false;
           transformHero(evolved: true, devolved:false);
           /*anim.Play("evolution-2");*/
           p_level2 = true;
           myCir.radius = 1.1f;
           /*groundCheck_hero2.enabled = true;*/
           myBox.size = new Vector2(1.584973f, 0.1599605f);
           myBox.offset = new Vector2(-0.01f, -1.02f);
       }
       else if (PlayerHealth == 25)
       {
           p_level1 = true;
           transformHero(evolved: false, devolved:true);
           p_level2 = false;
           myCir.radius = 0.5f;
           myBox.size = new Vector2(1.458524f, 0.1503876f);
           myBox.offset = new Vector2(0.08091533f, -0.51f);

       }
   }
   public void transformHero(bool evolved,bool devolved)
   {
       if (PlayerHealth == 50 && !evolved)
       {
           anim.Play("evolution-1");
           evolved = false;
       }

       if (PlayerHealth == 25 &&!devolved)
       {
           anim.Play("evolution-2");
           devolved = false;
       }
   }
}
