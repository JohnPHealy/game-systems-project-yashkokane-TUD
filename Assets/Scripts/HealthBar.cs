using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  public Slider slider;
 
  

  public void SetMaxHealth()
  {
    slider.maxValue = 100;
  }
  public void SetHealth(int health)
  {
      
      /*Debug.Log(health);*/
      slider.value = health;

  }
  
}
