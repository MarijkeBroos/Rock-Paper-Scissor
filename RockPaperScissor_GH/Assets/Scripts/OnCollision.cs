using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour 
{

	//On each collision the player makes with an enemy, the system checks the current texture of the player determining it's current status
	//Then checks the tag associated with the enemy and decides if it's a win or a loss. Nothing happens if the player is equal to the enemy.
	
	public Renderer currentMode; //Stores the current status derived from the player renderer. Gets updated on each collision.
	public GameController GameController;
	public PlayerController PlayerController;
	public Mover Mover;
		
	void OnTriggerEnter (Collider other)
    {
		currentMode = this.GetComponent<Renderer>(); 
				
		if((currentMode.material.mainTexture.name == "PlayerRock" && other.gameObject.tag == "Enemy_Paper")||(currentMode.material.mainTexture.name == "PlayerPaper" && other.gameObject.tag == "Enemy_Scissor")||(currentMode.material.mainTexture.name == "PlayerScissor" && other.gameObject.tag == "Enemy_Rock"))
		{
			GameController.LoseLife();
			PlayerController.GetHit();
		}
		
		if((currentMode.material.mainTexture.name == "PlayerRock" && other.gameObject.tag == "Enemy_Scissor")||(currentMode.material.mainTexture.name == "PlayerPaper" && other.gameObject.tag == "Enemy_Rock")||(currentMode.material.mainTexture.name == "PlayerScissor" && other.gameObject.tag == "Enemy_Paper"))
		{
			GameController.AddScore();	
			Destroy(other.gameObject);
		}
		
		else if ((currentMode.material.mainTexture.name == "PlayerRock" && other.gameObject.tag == "Enemy_Rock")||(currentMode.material.mainTexture.name == "PlayerPaper" && other.gameObject.tag == "Enemy_Paper")||(currentMode.material.mainTexture.name == "PlayerScissor" && other.gameObject.tag == "Enemy_Scissor"))
		{
			PlayerController.LightFade();
		}
    }
	

}
