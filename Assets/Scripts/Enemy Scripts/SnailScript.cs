using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D myBody;
    private Animator anim;

    public LayerMask playerLayer;
    private bool moveLeft = true;

    private bool canMove;
    private bool stunned;

    public Transform leftCollision, rightCollision, topCollision, downCollision;
    private Vector3 leftCollisionPosition, rightCollisionPosition, topCollisionPosition, downCollisionPosition;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftCollisionPosition = leftCollision.position;
        rightCollisionPosition = rightCollision.position;
        topCollisionPosition = topCollision.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                //     print("moveLeft");

                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            }
            else
            {
                //    print("else -- move right?");

                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }
        }
        CheckCollision();
    }

    void CheckCollision()
    {

        RaycastHit2D leftHit = Physics2D.Raycast(leftCollision.position, Vector2.left, .1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCollision.position, Vector2.right, .1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, .12f, playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.CompareTag(MyTags.Player))
            {
                if (!stunned)
                {
                    //hurt the player
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);  //update y velocity to 7f;
                    canMove = false;//stunned now
                    myBody.velocity = new Vector2(0, 0);
                    anim.Play("Stunned");
                    stunned = true;

                    //beetle code here
                    if (CompareTag(MyTags.Beetle))
                    {
                        anim.Play("Stunned");
                        StartCoroutine(Dead(.5f));
                    }

                }
            }
        }

        if (leftHit)
        {
            if (leftHit.collider.gameObject.CompareTag(MyTags.Player))
            {
                if (!stunned)
                {
                    print("hurt player");
                }
                else
                {
                    //fly off to the right
                    if (tag != MyTags.Beetle) //if snail
                    {
                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(2f));


                    }
                }
            }
        }

        if (rightHit)
        {
            if (rightHit.collider.gameObject.tag == MyTags.Player)
            {
                if (!stunned)
                {
                    //hurt player
                    print("hurt player");
                }
                else
                {
                    if (tag != MyTags.Beetle) //if snail
                    {
                        //fly off to the right
                        myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                        StartCoroutine(Dead(2f));
                    }

                }
            }
        }

        if (!Physics2D.Raycast(downCollision.position, Vector2.down, .1f))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);

            leftCollision.position = leftCollisionPosition;
            rightCollision.position = rightCollisionPosition;
        }
        else
        {
            //swap scale and collision positions
            tempScale.x = -Mathf.Abs(tempScale.x);

            leftCollision.position = rightCollisionPosition;
            rightCollision.position = leftCollisionPosition;
        }

        transform.localScale = tempScale;
    }

    //this hits snail no matter what
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        anim.Play("Stunned");
    //    }
    //}

    private IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(MyTags.Bullet))
        {

            if (CompareTag(MyTags.Beetle))
            {
                anim.Play("Stunned");

                canMove = false;
                myBody.velocity = Vector2.zero;

                StartCoroutine(Dead(.4f));
            }
            else if (CompareTag(MyTags.Snail))
            {
                print("shot the snail");
                if (!stunned)
                {
                    anim.Play("Stunned");
                    canMove = false;
                    stunned = true;
                    myBody.velocity = Vector2.zero;
                }
                else
                {
                    //dont kill shell.. bullets don't work on shell
                  //   StartCoroutine(Dead(.2f));
                   // gameObject.SetActive(false); //kill
                }




            }
        }
    }
}

