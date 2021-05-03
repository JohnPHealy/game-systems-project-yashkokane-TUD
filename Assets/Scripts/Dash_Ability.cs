using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dash_Ability : MonoBehaviour
{
    public PlayerMovement Dash;
    public TMP_Text name;
    public TMP_Text introText;
    public TMP_Text combatText;
    public TMP_Text dashText;
    public GameObject Dialogue;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dash.canDash = true;
            Destroy(gameObject);
            /*Debug.Log(("Dash"));*/
            name.enabled = true;
            dashText.enabled = true;
            
            combatText.enabled = false;
            introText.enabled = true;
        }
    }
    
  
            private void OnTriggerExit2D(Collider2D other)
                {
                    if (other.gameObject.tag == "Player")
                    {
                        name.enabled = false;
                        dashText.enabled = false;
                        Dialogue.SetActive(false);
                        
                    }
                }
}
