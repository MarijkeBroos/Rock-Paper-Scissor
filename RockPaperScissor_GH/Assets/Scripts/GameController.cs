using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour 
{
	// UI objects
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text lifeCounter;
	public Text feedback;
	
	//Game Objects
	public PlayerController Player;
	public SpawnerController Spawner;
	
	//Game status related values
	private bool restart;
	private int score;
	public int scoreValue;
	private int lifeSize;
	
	public float resetTime; //Set time for changing textures

	
	
	//Get's run on startup
	void Start()
	{
		//Starts the game with a custom windowed screen
		Screen.SetResolution(350, 525, false);
		
		restart = false;
		
		restartText.text = "";
		gameOverText.text = "";
		
		score = 0;
		UpdateScore(); 
		
		lifeSize = 5;
		UpdateLife();
		
		StartCoroutine(changeMode()); 
		
		feedback.text = "Start";
		UpdateFeedback();
	}
	
	//Runs every frame, as long as restart is false, pressing R will have no effect.
	void Update() 
	{
		if (restart)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}
	
	//Coroutine to change the Player texture over time set in "resetTime" 
	private IEnumerator changeMode()
     {
         while(true)
         {
			Player.changeTexture();
			
			//Every time the player changes mode, the enemies go faster to increase difficulty
			Spawner.UpdateEnemySpeed();
			
			yield return new WaitForSeconds(resetTime);
			

         }
     
     }

	//Adds points to the score
	public void AddScore()
	{
		score += scoreValue;
		UpdateScore();
	}
	
	//Detracts life from the life counter
	public void LoseLife()
	{
		feedback.text = "Ouch!";
		UpdateFeedback();
		
		lifeSize -= 1;
		if (lifeSize == 0)
		{
			GameOver ();
		}
		
		UpdateLife();
	}
	
	
	//Updates the score UI
	public void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
	
	//Updates the life UI
	public void UpdateLife()
	{
		lifeCounter.text = "Life: " + lifeSize;
	}
	
	//Updates the feedback UI
	public void UpdateFeedback()
	{
		StartCoroutine(ReadTime());
		
	}
	
	//Coroutine that controls how long message remains visible
	IEnumerator ReadTime()
	{
	 for(float i = 1 ; i >= 0f; i -=0.1f)
		{	
			yield return new WaitForSeconds(0.03f);
		}
		
		feedback.text = "";
	 }
	
	//Game over. The player can now press R to restart
	public void GameOver()
	{
		gameOverText.text = "Game Over";
		
		//Removes all enemies from the screen
		Spawner.StopSpawning();
		Spawner.DeleteEnemies();
		
		restart = true;
		restartText.text = "Press R to restart";
	}
}
