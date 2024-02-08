using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //public List<Vector2> availableDirections = new List<Vector2>() { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    //public LayerMask obstacleLayer;
    //public GameObject node;

    // Start is called before the first frame update
    private void Start()
    {
        //this.availableDirections = new List<Vector2>();
        //CheckDirection(Vector2.up);
        //CheckDirection(Vector2.down);
        //CheckDirection(Vector2.left);
        //CheckDirection(Vector2.right);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
        //Node node = collision.GetComponent<Node>();
        //Debug.Log("Node is nodeing");
        //if enemy collides with node and frightened is not enabled, scatter
        //if (collision.gameObject.tag == "Node")
        //{
            //Debug.Log("node is scattering");
            //pick a random direction from the index
            //int index = Random.Range(0, node.availableDirections.Count - 1);
            //if there's multiple ways to go, go to the next direction in the index
            //if (collision.gameObject.tag == ("Node") && node.availableDirections.Count > 0 && (index >= node.availableDirections.Count))
            //{
                //Debug.Log("node is flipping thru the index");
                //index++;
                //if it overflows, wrap back to the first direction index instance
                //if (index >= node.availableDirections.Count)
                //{
                    //index = 0;
                //}
            //}
        //}
    //}

    //private void CheckDirection(Vector2 direction)
    //{
        //RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * .5f, 0.0f, direction, 1.5f, obstacleLayer);
        //If it's not touching an occupied space, checks the available directions and takes one
        //if (hit.collider == null)
        //{
            //this.availableDirections.Add(direction);
        //}
    //}
}
