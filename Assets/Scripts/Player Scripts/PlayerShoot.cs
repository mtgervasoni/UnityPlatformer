using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private InputAction shootAction;
    private InputAction projectileAction;


    public GameObject bulletFire;
    public GameObject projectileFire;

    public float projectileSpeed = 5f;

    // Start is called before the first frame update
    void Awake()  //first function called when we run game (recall Animator panel)
    {
        shootAction = new InputAction("Shoot", InputActionType.Button, "<Gamepad>/buttonWest");
        projectileAction = new InputAction("Shoot", InputActionType.Button, "<Gamepad>/buttonEast");

    }


    void Start()
    {
        shootAction.Enable();
        projectileAction.Enable();
        //   print("jump action id:"+jumpAction.id.ToString());
        // jumpAction = new InputAction("Jump", InputActionType.Button, "<Gamepad>/buttonSouth");
    }
    private void Update()
    {
        ShootBullet();
    }
    void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.L) ||shootAction.triggered){
            GameObject bullet = Instantiate(bulletFire, transform.position, Quaternion.identity);  // create copy of bullet.. , ,000rotation (quaternion.identity
            var x = bullet.GetComponent<BulletFire>();
            x.Speed *= transform.localScale.x;
            print(x.Speed);
            print(transform.localScale.x);
        }

        if (projectileAction.triggered || Input.GetKeyDown(KeyCode.K))
        {
            print("SHOOT PROJECTILE");
            GameObject projectile = Instantiate(projectileFire, transform.position, Quaternion.identity);  // create copy of bullet.. , ,000rotation (quaternion.identity

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {

                // Calculate the direction of ||  the projectile (1, 1, 0 for up and to the side)
                Vector3 direction = new Vector3(transform.localScale.x, 1, 0).normalized;
                print(transform.localScale.x);

                // Apply initial velocity (speed) in the calculated direction
                rb.velocity = direction * projectileSpeed;
                print(rb.velocity);
            }


            //var x = projectile.GetComponent<ProjectileFire>();
            //x.Speed *= transform.localScale.x;
        }

    }
}
