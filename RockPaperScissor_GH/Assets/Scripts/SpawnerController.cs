using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour 
{
	//The Spawner game object manages the instantiation of the enemies 
	// Array of GameObject prefabs set in the Engine/Inspector
	public GameObject[] mode;

	// Spawntime settings
	public int spawnInterval;
	public float startWait;	
	
	public float moverEnemySpeed;

	
	// Runs on start-up game
	void Start () 
	{
		
		spawnInterval = 1;
		startWait = 1;
		moverEnemySpeed = 2f;
		
		//Repeats a function on declared start-up time startWait and with set interval spawnInterval
		InvokeRepeating("Spawn",startWait, spawnInterval);	
		
		
	}
	
	public void StopSpawning ()
	{
		CancelInvoke("Spawn");
	}
	
	public void DeleteEnemies ()
	{
		foreach (Transform child in transform) 
		{
			GameObject.Destroy(child.gameObject);
		}
	}
	
	public void UpdateEnemySpeed ()
	{
		moverEnemySpeed += 0.5f;
	}

	//Spawns the enemies
	void Spawn ()
	{

		//Randomises a spawnposition along the x-axis
		Vector3 position = new Vector3(Random.Range(-3.0f,3.0f), 0, 8.0f);
		
		//By default the enemy planes are standing upright and invisible for the top down camera, rotating them 90 degrees on creation fixes that
		Quaternion spawnRotation = Quaternion.Euler(90,0,0); 
		
		//Picks a random enemy from the array and instantiates it in the game 
		(Instantiate(mode[Random.Range (0,mode.GetLength (0))],position, spawnRotation)  as GameObject).transform.parent = this.transform;
	}
	
}
