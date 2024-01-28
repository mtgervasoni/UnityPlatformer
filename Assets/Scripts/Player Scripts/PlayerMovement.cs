using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10f;
    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private Rigidbody2D myBody;
    private Animator anim;
    [SerializeField]
    private InputAction jumpAction;


    private bool isGrounded;
    private int jumpCount = 0;
    private float jumpPower = 7f;

    bool xboxJumped;
    // Start is called before the first frame update

    // Declare a reference to the Jump action

    void Awake()  //first function called when we run game (recall Animator panel)
    {
        //rigid body attached on player body object..
        myBody = GetComponent<Rigidbody2D>(); //Get Component gets component attached to Player (ridigbody, box collider, etc)
        anim = GetComponent<Animator>();
        // jumpAction = InputActionAsset.
        jumpAction = new InputAction("Jump", InputActionType.Button, "<Gamepad>/buttonSouth");

    }
    private void OnEnable()
    {

    }

    void Start()
    {
        jumpAction.Enable();
        //   print("jump action id:"+jumpAction.id.ToString());
        // jumpAction = new InputAction("Jump", InputActionType.Button, "<Gamepad>/buttonSouth");
    }

    // Update is called once per frame
    void Update()
    {
        //if (jumpAction.triggered)
        //{
        //    if (xboxJumped == false)
        //    {
        //        // Perform jump logic here
        //        print("JUMP TRIGGERED!");
        //        xboxJumped = true;
        //    }
        //}
        //else
        //{
        //    xboxJumped = false;
        //}
        PlayerJump();
        CheckIfGrounded();


        //if(Physics2D.Raycast (groundCheckPosition.position, Vector2.down, .5f, groundLayer))
        //{
        //    print("collided with ground from update");
        //}
    }
    void FixedUpdate()
    {
        //physics calculation here
        //based on frame rate (defualt .02)

        PlayerWalk();

    }
    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale; //inital scale
        tempScale.x = direction;  //change it to 1 or -1..
        transform.localScale = tempScale;
    }
    void PlayerWalk()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            //this.position*= speed;
            ChangeDirection(1);
        }
        else if (x < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y); //stop movement (no coasting)
        }

        //set the speed.. which animates the character
        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    IEnumerator DelayedCheckGrounded()
    {
        isCheckingGrounded = true;
        yield return new WaitForSeconds(0.1f); // Adjust the delay as needed
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, .1f, groundLayer);
        if (isGrounded)
        {
            if (jumpCount > 0)
            {
                //  print("got here");
                anim.SetBool("Jump", false);
                jumpCount = 0;
            }
        }

        isCheckingGrounded = false;
    }

    private bool isCheckingGrounded = false;
    void CheckIfGrounded()
    {
        if (!isCheckingGrounded)
        {
            StartCoroutine(DelayedCheckGrounded());
        }

        //isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.right, .0001f, groundLayer);

        //if(isGrounded)
        //{
        //    if (jumpCount > 0 && pressedSpace) 
        //    {
        //        print("got here");
        //        anim.SetBool("Jump", false);
        //        jumpCount = 0;
        //        pressedSpace = false;
        //    }
        //}
    }
    void PlayerJump()
    {

        if (jumpCount < 2)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (jumpAction.triggered)
            {
                if (xboxJumped == false)
                {
                    // Perform jump logic here
                    Jump();
                    xboxJumped = true;
                }
            }
            else
            {
                xboxJumped = false;
            }

        }
    }

    void Jump()
    {
        print(jumpAction.id);
        anim.SetBool("Jump", true);
        print("pressed spacebar");
        jumpCount++;
        myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
    }
    //private void OnCollisionEnter2D(Collision2D collision)  //inherited from MonoBehavior
    //{
    //    //print(collision);
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //    print("collision with ground occured");
    //    }
    //    //called when collision occurs

    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "Ground")
    //    {
    //        print("collided with ground");
    //    }
    //}
}
