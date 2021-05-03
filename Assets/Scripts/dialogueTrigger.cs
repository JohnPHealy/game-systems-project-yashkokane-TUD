using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class dialogueTrigger : MonoBehaviour
{
    /*public GameObject dSystem;*/
    public TMP_Text name;
    public TMP_Text introText;
    public TMP_Text combatText;
    public TMP_Text dashText;
    public GameObject Dialogue;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Dialogue.SetActive(true);
            name.enabled = true;
            introText.enabled = true;
            
            combatText.enabled = false;
            dashText.enabled = false;
            
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                name.enabled = false;
                introText.enabled = false;
                Dialogue.SetActive(false);
                
            }
        }
}
