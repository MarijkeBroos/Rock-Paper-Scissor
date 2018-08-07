using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : SpawnerController 
{
	//This script is applied to all instantiated enemy player to make them move downward the screen
	
	public Color c;
	public Renderer rend;
	
	
	//Rigidbodies are needed to move the enemy
	private Rigidbody rb;
	
	// Runs on startup game
	void Start () 
	{
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody>();
		
		c = rend.material.color;
		c.a = 1f;
		
		rend.material.color = c;
	}
	
	//Runs every frame 
	void Update ()
	{
		rb.velocity = -transform.up * (transform.parent.GetComponent<SpawnerController>().moverEnemySpeed);
	}
}
