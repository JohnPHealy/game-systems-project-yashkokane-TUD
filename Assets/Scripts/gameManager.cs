using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 StartPos;
    public static int score;
    
    public TMP_Text scoretxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RespawnPlayer()
        {
            player.transform.position = StartPos;
        }
    
         public void PauseGame()
         {
             Time.timeScale = 0;
             
         }
    
         public void UnPauseGame()
         {
             Time.timeScale = 1;
         }
    
         public void RestartGame()
         {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
         }
         
         public void Quit()
         {
             //If we are running in a standalone build of the game
    #if UNITY_STANDALONE
             //Quit the application
             Application.Quit();
    #endif
    
             //If we are running in the editor
    #if UNITY_EDITOR
             //Stop playing the scene
             UnityEditor.EditorApplication.isPlaying = false;
    #endif
         }
}
