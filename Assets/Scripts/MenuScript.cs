using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene("level-1");
    }

    public void RestartGame()
    {
        PlayerMovement.PlayerHealth = 12;
        SceneManager.LoadScene("level-1");  
    }
    public void Quit()
    {
        SceneManager.LoadScene("Menu_Scene");
    }
}
