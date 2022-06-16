/******************************************************************************
Name:          
Description:   
               
Author(s):     
Date:          
Version:       V1.0 
TODO:          - 
               - 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
  public static GameAssets i;  
  private void Awake () {
      i = this;
  }

  public Sprite snakeHeadSprite;
  public Sprite foodSprite;
  public Sprite snakeBodySprite;
}