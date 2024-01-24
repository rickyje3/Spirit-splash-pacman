using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rgbd;
    private int playerDirection;
    bool holdDownMovementKey;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxisRaw("Horizontal");
        float verMovement = Input.GetAxisRaw("Vertical");

        ConstantMovement();
        rgbd.AddForce(new Vector2(playerDirection * hozMovement * speed, verMovement * speed));
    }

    void ConstantMovement()
    {
        rgbd.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        
        if (holdDownMovementKey == true)
        {
            Input.GetKey("down");
        }

    }
}
