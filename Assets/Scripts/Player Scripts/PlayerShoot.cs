using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private InputAction shootAction;

    public GameObject bulletFire;
    // Start is called before the first frame update
    void Awake()  //first function called when we run game (recall Animator panel)
    {
        shootAction = new InputAction("Shoot", InputActionType.Button, "<Gamepad>/buttonWest");
    }


    void Start()
    {
        shootAction.Enable();
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
        }

    }
}
