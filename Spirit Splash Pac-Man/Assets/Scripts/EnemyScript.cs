using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 2.9f;
    //public int points = 500;
    public EnemyHome home { get; private set; }
    public EnemyScatter scatter { get; private set; }
    public EnemyChase chase { get; private set; }
    public EnemyFrightened frightened { get; private set; }
    public EnemyBehavior initialBehavior;
    //object you want them to lock on to
    public Transform player;
    public GameObject enemy;
    public Transform enemyRespawn;
    public Transform enemySpawn;
    public Vector2 initialDirection;
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }
    public Rigidbody rgbd { get; private set; }
    public PlayerScript movement { get; private set; }
    public GameObject node;
    private float distance;
    public LayerMask obstacleLayer;

// Start is called before the first frame update
private void Start()
    {
        home = GetComponent<EnemyHome>();
        scatter = GetComponent<EnemyScatter>();
        chase = GetComponent<EnemyChase>();
        frightened = GetComponent<EnemyFrightened>();
        movement = GetComponent<PlayerScript>();
        StartLevelSpawn();
        GameObject.FindGameObjectsWithTag("Node");
        GameObject.FindGameObjectsWithTag("Player");

    }

    // Update is called once per frame
    private void Update()
    {
        //distance = Vector2.Distance(transform.position, player.transform.position);
        //Vector2 direction = player.transform.position - transform.position;

        //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        if (player != null)
        {
            // Move towards the target (Pac-Man)
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    public void StartLevelSpawn()
    {
        enemy.transform.position = enemySpawn.position;
    }

    public void ResetState()
    {
        enemy.transform.position = enemyRespawn.position;
        //When movement needs to be cancelled this will not be enabled
        enabled = true;
        //home.Enable();
        //chase.Disable();
        //scatter.Enable();
        //frightened.Disable();
    }

    //Defines the occupied tile space
    public bool Occupied(Vector2 direction)
    {
        //boxcast, the players position, size of the box scaled by 75%, no angle, direction, distance,
        //check boxcast on obstacle layer
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * .75f, 0.0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }
}
