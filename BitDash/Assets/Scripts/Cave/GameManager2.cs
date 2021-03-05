/*
 * This file is the Manager of the Game. version 1.0 27/10/2020 @Xiaoyan Zhou
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    //game status
    public enum GameState
    {
        Start,
        Stop,
    }
    
    //initial gamestate
    public GameState gameState = GameState.Start;
    public Button reStartButton;
    //initial title
    public Text title;
    //import class for player
    public Player2 player;
    //Game state
    public static bool GameStart = false;
    
    //bool timelock
    public bool timelock;
    
    //score text components
    public Text scoreText;
    public float scoreAmount;
    public float pointIncreasedPerSecond;
    
    // Start is called before the first frame update
    void Start()
    {
        // //The click event for player
        reStartButton.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        player = GameObject.Find("idle").GetComponent<Player2>();
        StartGame();
        //player class
        
        
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //change score text per frame
        scoreText.text = "Score: " + (int) scoreAmount;
        if(timelock == false)
            
            scoreAmount += pointIncreasedPerSecond * 50 * Time.deltaTime;

    }
    
    //Quit game
     private void Quit()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    
    //This is the method for start the game
    private void StartGame()
    {
        //initialize score text and amount
        scoreText.text = "Score";
        scoreAmount = 0f;
        pointIncreasedPerSecond = 1f;
        
        
        //if the game state is Stop, start the game
        //set game state and hide buttons and title
            gameState = GameState.Start;
            GameStart = true;
            //prevent from rotating
            player.rigidBody.freezeRotation = true;
            //start run
            player.Run();
        
    }
    
    //GameOver function
    public void GameOver()
    {
        //Player is dead
        if (player.playerDeath == true)
        {
            title.text = "Game Over";
            title.gameObject.SetActive(true);
            reStartButton.gameObject.SetActive(true);
            gameState = GameState.Stop;
            GameStart = false;
            timelock = true;
        
            reStartButton.onClick.AddListener(Restart);
        }

        if (player.playerWin == true)
        {
            SceneManager.LoadScene(3);
        }
    }

    //Restart game
    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
