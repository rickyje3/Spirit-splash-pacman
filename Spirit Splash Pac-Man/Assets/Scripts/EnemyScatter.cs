using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScatter : EnemyBehavior
{
    //public GameObject Node;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Node node = collision.GetComponent<Node>();
        //Debug.Log("Node is nodeing");
        //if enemy collides with node and frightened is not enabled, scatter
        //if (collision.gameObject.tag == "Node" && !this.enemy.frightened.enabled)
        //{
            //Debug.Log("node is scattering");
            //pick a random direction from the index
            //int index = Random.Range(0, node.availableDirections.Count - 1);
            //if there's multiple ways to go, go to the next direction in the index
            //if (collision.gameObject == (Node) && node.availableDirections.Count > 0 && (index >= node.availableDirections.Count))
            //{
                //Debug.Log("node is flipping thru the index");
                //index++;
                //if it overflows, wrap back to the first direction index instance
                //if (index >= node.availableDirections.Count)
                //{
                   // index = 0;
                //}
            //}
            //enemy.movement.SetDirection(node.availableDirections[index]);
        //}
    }

    public void move()
    {

    }
}
