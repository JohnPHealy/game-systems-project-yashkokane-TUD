using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    public PlayerMovement _SuperJump;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _SuperJump.canSJ = true;
            Destroy(gameObject);
            /*Debug.Log(("Dash"));*/
            /*name.enabled = true;
            dashText.enabled = true;
            
            combatText.enabled = false;
            introText.enabled = true;*/
        }
    }
}
