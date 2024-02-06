using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerScript : MonoBehaviour
{
    public float speed = 3.0f;
    public TMP_Text scoreText;
    public TMP_Text duckText;
    public int scoreValue { get; private set; }
    public GameObject player;
    public GameObject winText;
    public GameObject loseText;
    public Rigidbody2D rgbd { get; private set; }
    public static bool isGameOver;
    public Vector2 initialDirection;
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector2 startingPosition { get; private set; }
    public GameObject Duck;
    public GameObject SuperDuck;
    public int DuckLimit;
    private int DuckValue;
    private bool isSuperMode;
    private int lives;
    public TMP_Text livesText;
    public Transform playerRespawn;
    public LayerMask obstacleLayer;
 
    
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        ResetState();
        //startingPosition = transform.position;
        scoreValue = 0;
        winText.SetActive(false);
        loseText.SetActive(false);
        isGameOver = false;
        DuckValue = 0;
        isSuperMode = false;
        lives = 3;
    }

    void ResetState()
    {
        player.transform.position = playerRespawn.position;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        rgbd.isKinematic = false;
        //When movement needs to be cancelled this will not be enabled
        enabled = true;
    }

    void Update()
    {
        Movement();
        ConstantMovement();
        ChangeDirection();
        CheckIfDucksLeft();
        scoreText.SetText("Points: " + scoreValue);
        duckText.SetText("Ducks Caught: " + DuckValue);
        livesText.SetText("Lives: " + lives);

        if (isGameOver == true)
        {
            //Time.timeScale = 0;
        }

        //if next direction isn't 0, set as current direction
        //it will try every frame to go in that direction until the layer is unoccupied
        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rgbd.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        this.rgbd.MovePosition(position + translation);
    }


    //Checks if the tile in the direction is occupied
    public void SetDirection(Vector2 direction, bool forced = false)
    {   //if you force a direction change or the tile in direction is not occupied
        if (forced || !Occupied(direction))
        {
            //change direction
            this.direction = direction;
            //clear the direction queue
            nextDirection = Vector2.zero;
        }
        else
        {
            //queue next direction
            nextDirection = direction;
        }
    }

    //Defines the occupied tile space
    public bool Occupied(Vector2 direction)
    {
        //boxcast, the players position, size of the box scaled by 75%, no angle, direction, distance,
        //check boxcast on obstacle layer
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * .75f, 0.0f,
        direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }

    //When colliding with the duck, destroy it and add a point to score
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the gameObject is tagged "duck" destroy it once you collide and add to score
        if (collision.gameObject.tag == "Duck")
        {
            //each time you collide increase score by 100
            scoreValue += 100;
            Debug.Log(scoreValue + " Points");
            DuckValue += 1;
            Debug.Log(DuckLimit + "Ducks Caught");
            Destroy(collision.collider.gameObject);
        }
        else if (collision.gameObject.tag == "SuperDuck")
        {
            //each time you collide increase score by 300
            scoreValue += 300;
            Debug.Log(scoreValue + " Points");
            DuckValue += 1;
            Debug.Log( DuckLimit + "Ducks Caught");
            Destroy(collision.collider.gameObject);
        }
        else if (collision.gameObject.tag == "Food")
        {
            //each time you collide increase score by 500
            scoreValue += 500;
            Destroy(collision.collider.gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            if (isSuperMode == true)
            {
                scoreValue += 1000;
                Destroy(collision.collider.gameObject);
            }
            if (lives > 0 && collision.gameObject.tag == "Enemy")
            {
                --lives;
                Invoke(nameof(Respawn), 3.0f);     
            }
            else
            {
                Destroy(gameObject);
                //isGameOver = true;
            }
        }
    }

    void Respawn()
    {
        rgbd.velocity = Vector2.zero;
        player.transform.position = playerRespawn.position;
    }

    //Keeps the player moving after the input is pressed
    void ConstantMovement()
    {
        rgbd.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
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

    //Changes the direction the player is facing
    void ChangeDirection()
    {
        //faces player left
        if (direction == Vector2.left)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //faces player right
        if (direction == Vector2.right)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void CheckIfDucksLeft()
    {
        if(DuckValue >= 180)
        {
            isGameOver = true;
            winText.SetActive(true);
            speed = 0;
        }
    }
}
