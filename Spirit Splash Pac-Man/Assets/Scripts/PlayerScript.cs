using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed = 3.0f;
    public TMP_Text scoreText;
    public TMP_Text duckText;
    public GameObject nextLevelText;
    public int scoreValue { get; private set; }
    public GameObject player;
    public GameObject BlueKnight;
    public GameObject GreenKnight;
    public GameObject PinkKnight;
    public GameObject YellowKnight;
    private GameManager enemies;
    public GameObject winText;
    public GameObject loseText;
    public Rigidbody2D rgbd { get; private set; }
    public static bool isGameOver;
    public Vector2 initialDirection;
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }
    public GameObject Duck;
    public GameObject SuperDuck;
    public int DuckLimit;
    private int DuckValue;
    public bool isSuperMode;
    public float superModeTimer { get; private set; }
    public float timeSuper = 10.0f;
    private int lives = 3;
    public TMP_Text livesText;
    public Transform playerRespawn;
    public Transform enemyRespawn;
    public LayerMask obstacleLayer;
    EnemyScript enemyScript;


    // Start is called before the first frame update
    private void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        ResetState();
        //startingPosition = transform.position;
        scoreValue = 0;
        winText.SetActive(false);
        loseText.SetActive(false);
        nextLevelText.SetActive(false);
        isGameOver = false;
        DuckValue = 0;
        isSuperMode = false;
    }

    private void ResetState()
    {
        player.transform.position = playerRespawn.position;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        rgbd.isKinematic = false;
        //When movement needs to be cancelled this will not be enabled
        enabled = true;
    }

    private void BlueKnightResetState()
    {
        BlueKnight.transform.position = enemyRespawn.position;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        rgbd.isKinematic = false;
        //When movement needs to be cancelled this will not be enabled
        enabled = true;
    }

    private void YellowKnightResetState()
    {
        YellowKnight.transform.position = enemyRespawn.position;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        rgbd.isKinematic = false;
        //When movement needs to be cancelled this will not be enabled
        enabled = true;
    }

    private void PinkKnightResetState()
    {
        PinkKnight.transform.position = enemyRespawn.position;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        rgbd.isKinematic = false;
        //When movement needs to be cancelled this will not be enabled
        enabled = true;
    }

    private void GreenKnightResetState()
    {
        GreenKnight.transform.position = enemyRespawn.position;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        rgbd.isKinematic = false;
        //When movement needs to be cancelled this will not be enabled
        enabled = true;
    }

    private void Update()
    {
        Movement();
        ConstantMovement();
        ChangeDirection();
        CheckIfDucksLeft();
        CheckIfPlayerDead();
        scoreText.SetText("Points: " + scoreValue);
        duckText.SetText("Ducks Caught: " + DuckValue);
        livesText.SetText("Lives: " + lives);

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.UnloadSceneAsync("SampleScene");
            SceneManager.LoadScene("Level2");
        }

        //brings u to level two when u press the spacebar when u collect all the ducks
        if (isGameOver == true)
        {
            Time.timeScale = 0;
            speed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.UnloadSceneAsync("SampleScene");
                SceneManager.LoadScene("Level2");                
            }
        }

       

        //if next direction isn't 0, set as current direction
        //it will try every frame to go in that direction until the layer is unoccupied
        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (isSuperMode == true)
        {
            superModeTimer -= Time.deltaTime;
            if (superModeTimer < 0)
            {
                isSuperMode = false;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rgbd.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rgbd.MovePosition(position + translation);
    }


    //Checks if the tile in the direction is occupied
    public void SetDirection(Vector2 direction)
    {   //if you force a direction change or the tile in direction is not occupied
        if (!Occupied(direction))
        {
            //change direction
            this.direction = direction;
            //clear the direction queue
            this.nextDirection = Vector2.zero;
        }
        else
        {
            //queue next direction
            this.nextDirection = direction;
        }
    }

    //Defines the occupied tile space
    public bool Occupied(Vector2 direction)
    {
        //boxcast, the players position, size of the box scaled by 75%, no angle, direction, distance,
        //check boxcast on obstacle layer
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * .75f, 0.0f, direction, 1.5f, obstacleLayer);
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
            //ADD A DUCK SOUND TO THIS
        }
        if (collision.gameObject.tag == "SuperDuck")
        {
            //each time you collide increase score by 300
            scoreValue += 300;
            Debug.Log(scoreValue + " Points");
            DuckValue += 1;
            Debug.Log(DuckLimit + "Ducks Caught");
            //Destroy(collision.collider.gameObject);
            isSuperMode = true;
            Debug.Log("SuperMode is on");
            superModeTimer = timeSuper;
            Destroy(collision.collider.gameObject);
            //ADD A SOUND TO THIS
        }
        if (collision.gameObject.tag == "Food")
        {
            //each time you collide increase score by 500
            scoreValue += 500;
            Destroy(collision.collider.gameObject);
            //ADD A SOUND TO THIS
        }
        
        //ADD A SOUND TO THIS (Player running into enemy)
        if (collision.gameObject.tag == "Blue")
        {
            //ADD A SOUND TO THIS (Player hitting the enemy with the super duck effect which kills it)
            if (isSuperMode == true)
            {
                scoreValue += 500;               
                BlueKnightResetState(); 
            }
            if (isSuperMode == false)
            {
                --lives;
                Respawn();
            }

        }

        //ADD A SOUND TO THIS (Player running into enemy)
        if (collision.gameObject.tag == "Yellow")
        {
            //ADD A SOUND THIS (Player hitting the enemy with the super duck effect which kills it)
            if (isSuperMode == true)
            {
                scoreValue += 500;
                YellowKnightResetState();
            }
            if (isSuperMode == false)
            {
                --lives;
                Respawn();
            }
        }

        //ADD A SOUND TO THIS (Player running into enemy)
        if (collision.gameObject.tag == "Green")
        {
            //ADD A SOUND TO THIS (Player hitting the enemy with the super duck effect which kills it)
            if (isSuperMode == true)
            {
                scoreValue += 500;
                GreenKnightResetState();
            }
            if (isSuperMode == false)
            {
                --lives;
                Respawn();
            }

        }

        //ADD A SOUND TO THIS (Player running into enemy)
        if (collision.gameObject.tag == "Pink")
        {
            //ADD A SOUND TO THIS (Player hitting the enemy with the super duck effect which kills it)
            if (isSuperMode == true)
            {
                scoreValue += 500;
                PinkKnightResetState();
            }
            if (isSuperMode == false)
            {
                --lives;
                Respawn();
            }
        }
    }

    private void Respawn()
    {
        rgbd.velocity = Vector2.zero;
        direction = Vector2.zero;
        player.transform.position = playerRespawn.position;
    }

    //Keeps the player moving after the input is pressed
    private void ConstantMovement()
    {
        rgbd.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    //if input is pressed move in said direction
    private void Movement()
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
    private void ChangeDirection()
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

    //ADD A WIN SOUND TO THIS
    private void CheckIfDucksLeft()
    {
        if(DuckValue >= 176)
        {
            isGameOver = true;
            winText.SetActive(true);
            speed = 0;
            nextLevelText.SetActive(true);
        }
    }

    //ADD A LOSS SOUND TO THIS
    private void CheckIfPlayerDead()
    {
        if (lives <= 0)
        {
            loseText.SetActive(true);
            isGameOver = true;
            player.gameObject.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this loads the currently active scene
            }                     
        }
    }
}
