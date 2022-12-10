using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    [Header("Detection Parameters")]
    
    //Detection Point
    public Transform detectionPoint;
    
    //Detection Radius
    private const float detectionRadius = 0.2f;
    
    //Detection Layer
    public LayerMask detectionLayer;
    
    //Cached Trigger object
    public GameObject detectedObject;
    
    //list of picked items
    public List<GameObject> pickedItems= new List<GameObject>();
    
    [Header("Examine Fields")]
    public GameObject examineWindow;
    public Image examineImage;
    public bool isExamining;
    public Text examineText;
    
    //If Algo's item collider interacts with the detectedObject's collider, then initiate the Interact
    //function
    void Update()
    {
        if(DetectObject())
        {
            if(InteractInput())
            {
                detectedObject.GetComponent<Item>().Interact();
            }
        }
    }
    
    //When player presses E, interact is initiated
    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {     
        //Set the parameters for the collider of an object
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        //If obj does not exist, do nothing
        if (obj == null)
        {
            detectedObject = null;
            return false;
        }
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }
       
    }

    public void PickUpItem(GameObject item)
    {
        //Add the item to the player's inventory (in InventorySystem script)
        pickedItems.Add(item);
    }

    public void ExamineItem(Item item)
    {
        if (isExamining) { 
            //Hide an examine window
            examineWindow.SetActive(false);
            //Disable the boolean
            isExamining = false;
        }
        else
        {
            //Show the item's image
            examineImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            //Write text underneath the image
            examineText.text = item.descriptionText;
            //Display the examine window
            examineWindow.SetActive(true);
            //Enable the boolean
            isExamining = true;
        }
    }
}

