using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{ 
    private Transform passage;

    public bool isLeft;

    public float distance = 0.2f;

    public Collider2D destination;

    // Start is called before the first frame update
    private void Start()
    {
        //if it's not the left passage that means it's the right passage, teleport to the left passage
        if (isLeft == false)
        {
            passage = GameObject.FindGameObjectWithTag("LeftPassage").GetComponent<Transform>();
        }
        else
        {
            passage = GameObject.FindGameObjectWithTag("RightPassage").GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }


    //You could add a sound to this if you want to i guess
    private void OnTriggerEnter2D(Collider2D destination)
    {
        //if distance from the gameobjec
        if (Vector2.Distance(transform.position, passage.transform.position) > distance)
        {
            destination.transform.position = new Vector2(passage.position.x, passage.position.y);
        }
    }
}
