using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    //Initiate an enumerator of the three object types for gameObject
    public enum InteractionType {NONE, PickUp, Examine};
    
    public InteractionType type;
    public string descriptionText;
    public Sprite image;
    
    //Create a customEvent object (specific in-game action) 
    public UnityEvent customEvent;
    
    private void Reset()
    {
        //Set isTrigger status of Collider2D to true
        GetComponent<Collider2D>().isTrigger = true;
        //Assign LayerMask of item to 9
        gameObject.layer = 9;
    }

    public void Interact()
    {
        switch(type)
        {
            case InteractionType.PickUp:
                //Add the object to the PickedUpItemsList
                GameObject item = gameObject;
                FindObjectOfType<InteractionSystem>().PickUpItem(gameObject);
                //Disable the item
                gameObject.SetActive(false);
                break;

            case InteractionType.Examine:
                //Examine the object instance through the ExamineWindow UI
                FindObjectOfType<InteractionSystem>().ExamineItem(this);
                break;

            default:
                Debug.Log("NULL");
                break;

        }
        //Call the custom Event function
        customEvent.Invoke();
    }
}
