using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    private int score;
    private Rigidbody2D rgbd;
    bool holdDownMovementKey;
    bool isGameOver;
    private Vector2 direction = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        score = 0;
        isGameOver = false;
    }

    void Update()
    {
        Movement();
        ConstantMovement();
    }

    //When colliding with the duck, destroy it and add a point to score
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the gameObject is tagged "duck" destroy it once you collide
        if (collision.collider.tag == "Duck")
        {
            //each time you collide increase score by 1
            score += 1;
            //changes score to the string integer value
            Destroy(collision.collider.gameObject);
        }
    }

    //Keeps the player moving after the input is pressed
    void ConstantMovement()
    {
        rgbd.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;
    }

    //if input is pressed move in said direction
    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
    }
}
