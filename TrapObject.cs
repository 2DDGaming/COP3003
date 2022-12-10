using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class TrapObject: MonoBehaviour
{
    private void Reset()
    {
        //Set the isTrigger status of the BoxCollider2D class to true
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      //If the collider of the trap object interacts with the Algo's object collider, 
      //then Algo loses a life
        if(collision.tag == "Player")
        {
            FindObjectOfType<LifeCount>().LoseLife();

        }
    }
}
