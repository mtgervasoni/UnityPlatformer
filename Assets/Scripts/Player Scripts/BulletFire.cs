using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{

    private float speed = 10f; //bullet speed
    private Animator anim;

    private bool canMove;

    private void Awake()
    {
        anim = GetComponent<Animator>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
      // StartCoroutine(DisableBullet(5f)); 
    }

    // Update is called once per frame
    void Update()
    {
        Move();  
    }

    void Move()
    {
        if (canMove)
        {
            //  print("Move 29");
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    IEnumerator DisableBullet(float timer)
    {
        print("Disable Bullet");
        yield return new WaitForSeconds(timer);
       // enabled = false;
        print("enabled set to false");

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //bullet will be disbaled
        if(collision.gameObject.CompareTag(MyTags.Snail) || collision.gameObject.CompareTag(MyTags.Beetle))
        {
            anim.Play("Explode");
            canMove = false;
            enabled = false;

           StartCoroutine(DisableBullet(.1f));
        }
    }

}
