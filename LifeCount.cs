using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    //Create a dynamic array of PlayerLives (dependent on number of Life images)
    public Image[] lives;
    public int livesRemaining;

    //3 lives - 3 images (0,1,2)

    public void LoseLife()
    {
      //If the player is dead, do nothing
        if (livesRemaining == 0)
            return;
            
        //Decrease the value of livesRemaining
        livesRemaining--;
        
        //Hide one of the heart (life) images
        lives[livesRemaining].enabled = false;
        
        //If no lives left, the game is lost
        if(livesRemaining == 0)
        {
            FindObjectOfType<Algo>().Die();
        }
    }
}
