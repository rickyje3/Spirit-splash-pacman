using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    private static int boardWidth = 31;
    private static int boardHeight = 19;
    //For every node iterated, store it in the array x and y. They are located between boardwidth and height
    public GameObject[,] board = new GameObject[boardWidth, boardHeight];

    // Start is called before the first frame update
    void Start()
    {
        Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
