using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackedHero : MonoBehaviour
{
    public PlayerMovement health;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.Play("Enemy_attack");
            health.updateHealth1();
            StartCoroutine(attackHalt());
            
            /*Destroy(gameObject);*/
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        anim.Play("Enemy_walk");
    }

    IEnumerator attackHalt()
    {
        yield return new WaitForSeconds(3f);
    }
}
