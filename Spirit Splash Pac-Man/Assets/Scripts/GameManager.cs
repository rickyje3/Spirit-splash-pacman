using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyScript[] enemies;
    public PlayerScript player;
    public Transform ducks;
    PlayerScript isGameOver;

    public int lives { get; private set; }

    void Start()
    {
        NewGame();
        PlayerScript.isGameOver = false;
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        NewLevel();
    }

    private void NewLevel()
    {
        //Resets the ducks when a new level starts
        foreach(Transform ducks in this.ducks)
        {
            ducks.gameObject.SetActive(true);
        }

        ResetState();
    }

    public void ResetState()
    {
        //Resets the enemies when a new level starts, if enemies is less than original number it adds enemies until it reaches that number
        for (int i = 0; i < this.enemies.Length; i++)
        {
            this.enemies[i].gameObject.SetActive(true);
        }

        //Resets the player when a new level starts
        this.player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        PlayerScript.isGameOver = true;

        //Does not reset the enemy count when the level ends
        for (int i = 0; i < this.enemies.Length; i++)
        {
            this.enemies[i].gameObject.SetActive(false);
        }

        //Does not reset the player count when the level ends
        this.player.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void EnemyDefeat(PlayerScript Enemy)
    {
        
    }

    public void PlayerDefeat()
    {
        this.player.gameObject.SetActive(false);
        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            //Delays reset state by 3 seconds before calling
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }

    }
}
