//Based on tutorials uploaded by AntarYT. This script was created for defining player object's (Algo's) actions
and physical parameters. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 //Algo is a class of the master class Monobehaviour 
public class Algo : MonoBehaviour
{
    //create a private object rb of the Rigidbody2D class
    private Rigidbody2D rb; 
    //store and manipulate the position of the groundCheck collider
    [SerializeField] Transform groundCheckCollider;
    //specify the groundLayer to use in a Physics.Raycast
    [SerializeField] LayerMask groundLayer;
    //specify the standingCollider based on the Collider2D class
    [SerializeField] Collider2D standingCollider;
    //specify the crouchingCollider based on the Collider2D class
    [SerializeField] Collider2D crouchingCollider;
    //store and manipulate the position of the overheadCheck collider
    [SerializeField] Transform overheadCheckCollider;

    //specify the radius of the groundCheck collider
    const float groundCheckRadius = 0.2f;
    //specify the radius of the overheadCheck collider
    const float overheadCheckRadius = 0.2f;
    //store the value of jumpPower for the Algo object
    [SerializeField] float jumpPower = 350;
    float horizontalValue;
    [SerializeField] public float speed = 3;
    //store the value of totalJumps allocated for the Algo object
    [SerializeField] int totalJumps;
    //store the value of availableJumps out of the totalJumps for the Algo object
    int availableJumps;
    float crouchSpeedModifier = 0.5f;
    
    //set rightward direction as default for Algo object
    bool facingRight = true;
    //set (physical) grounding of Algo object as default to true
    [SerializeField] bool isGrounded = true;
    bool crouchPressed;
    bool multipleJump;
    //set the player's isDead status to a default false
    bool isDead = false;

    private void Reset()
    {
        //The Algo object can react to other objects, but are not going to use Physics
        GetComponent<Collider2D>().isTrigger = true;
        //Specify layerMask for Algo and its children objects
        gameObject.layer = 10;
    }

    void Awake()
    {
        availableJumps = totalJumps;
        //Object rb is assigned to the Rigidbody2D class
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Do nothing if the player is interacting with object or dead
        if (CanMoveOrInteract() == false)
            return;
        //Set horizontal value equal to the horizontal input 
        horizontalValue = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetButtonDown("Crouch"))
        {
            crouchPressed = true;
        }

        else if (Input.GetButtonUp("Crouch"))
        {
            crouchPressed = false;
        }
    }

    //Updates based on a fixed amount of time
    void FixedUpdate()
    {
        GroundCheck(); //checks if the player isGrounded
        Move(horizontalValue, crouchPressed); 
    }

    bool CanMoveOrInteract()
    {
        bool can = true; //by default the player can interact with items
        
        //Situations when the player loses ability to move or interact
        if (FindObjectOfType<InteractionSystem>().isExamining)
            can = false;
        if (isDead)
            can = false;

        return can;
    }

    void Jump()
    {   //The Algo object is colliding with the ground object
        if (isGrounded)
        {
            multipleJump = true;
            availableJumps--;

            rb.velocity = Vector2.up * jumpPower;
        }

        else
        { //Algo object is in the air and has available jumps left
            if (multipleJump && availableJumps > 0)
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
            }
        }
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        //Check if GroundCheckObject is colliding with other 2D colliders
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        
        //The groundCheckObject is colliding with another 2D collider
        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                availableJumps = totalJumps;
                multipleJump = false;
            }
        }
    }

    void Move(float dir, bool crouchFlag)
    {
        //By default, the player is not crouching
        if (!crouchFlag)
        {   //Check if the overheadCheck collider is interacting an object collider, and set flag accordingly
            if (Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer))
                crouchFlag = true;
        }
        standingCollider.enabled = !crouchFlag;
        crouchingCollider.enabled = crouchFlag;

        float xVal = dir * speed * Time.deltaTime * 100;

        if (crouchFlag)
            xVal *= crouchSpeedModifier;

        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y); //set the speed of the Algo object
        rb.velocity = targetVelocity;


        //if looking right and clicked left (flip to the left)
        if (facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        //If looking left and clicked right (flip to the right)
        else if (!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
    }

    public void Die()
    {
        isDead = true;
        FindObjectOfType<LevelManager>().Restart(); //reset the player's position and lives
    }

    public void ResetPlayer()
    {
        isDead = false;
    }
}
