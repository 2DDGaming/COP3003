using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //define the fillBar image of the HealhBar UI
    public Image fillBar;
    public float health;

    //100 health => 1 fill amount

    public void LoseHealth(int value)
    {
     //if Algo(player) is dead, the health bar is empty and cannot be changed
        if (health <= 0)
            return;
        //Reduce the health
        health -= value;
        //Refresh the UI fillBar
        fillBar.fillAmount = health / 100;
        //Check if the health is zero or less 
        if (health <= 0)
        {
        //Algo(player) is dead and its position is reset
            FindObjectOfType<Algo>().Die();
        }
    }
}
