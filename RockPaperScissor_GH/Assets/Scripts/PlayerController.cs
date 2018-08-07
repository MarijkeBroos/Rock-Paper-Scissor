using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax;
}

public class PlayerController : MonoBehaviour 
{
	//UI
	public Text feedback;
	
	//Variables needed for movement
	private Rigidbody rb;
	public float speed;
	public Boundary boundary;
	
	
	//Variables needed for changing texture overtime
	public Texture[] playerTexture; //Array with textures. Size and content set in Engine Inspector
	public Renderer rend;
	public Color c;
	public Collider col;
	
	
	
	//Called on startup of the game
	void Start()
	{
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
		
		c = rend.material.color;
		c.a = 1f;
		c.r = 1f;
		c.g = 1f;
		c.b = 1f;
	}
	
	
	
	//Gets activated when A,D or the horizontal arrow keys get pressed. 
	//The default Unity script assigns the keys to the axis and moves the player accordingly
	void FixedUpdate () 
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
	
		Vector3 movement = new Vector3(moveHorizontal,0.0f, 0.0f);
		rb.velocity = movement * speed;
		
		rb.position = new Vector3 
		(
		//Clamp prevents the value from moving outside the boundaries 
		//Preventing the player from moving off the screen
		Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
		0.0F,
		0.0F
		);
	}

	
	
	//Chooses a random texture from the array and assigns this to the player
	//Player fades in and out when the texture changes
	public void changeTexture () 
     {
		StartCoroutine(FadeOut());
		this.rend.material.mainTexture = playerTexture[Random.Range(0, playerTexture.Length)];
		StartCoroutine(FadeIn());

     }
	
	IEnumerator FadeOut()
	 {
	 for(float j = 1 ; j >= 0f; j -=0.1f)
		{	
			this.c.a = j;
			this.rend.material.color = c;
			yield return new WaitForSeconds(0.3f);
		}	
	 }
	 
	 IEnumerator FadeIn()
	 {
	 for( float i = 0f ; i <= 1f; i +=0.1f)
		{	
			this.c.a = i;
			this.rend.material.color = c;
		    yield return new WaitForSeconds(0.3f);
		}
	 }
	
	
	
	//Player gets red, when hit	
	public void GetHit()
	{
		StartCoroutine(GoRed());
		StartCoroutine(LeaveRed());
	}
	  
	 IEnumerator GoRed()
	 {
		col.enabled = false;
		for(float m = 1 ; m >= 0f; m -=0.1f)
		{	
			this.c.g = c.b = m;

			this.rend.material.color = c;
			yield return new WaitForSeconds(0.1f);
		}	
	 }
	 
	 IEnumerator LeaveRed()
	 {
		for( float n = 0f ; n <= 1f; n +=0.1f)
		{	
			this.c.g = c.b = n;
			this.rend.material.color = c;
		    yield return new WaitForSeconds(0.1f);
		}
		col.enabled = true;
	 }
	
	
	
	//When it's a tie, player gets transparant
	public void LightFade()
	 {
		 StartCoroutine(LightFadeOut());
		 StartCoroutine(LightFadeIn());
	 }
	
	IEnumerator LightFadeOut()
	 {
		 col.enabled = false;
		for(float k = 1f ; k >= 0.5f; k -=0.1f)
		{	
			this.c.a = k;
			this.rend.material.color = c;
			yield return new WaitForSeconds(0.1f);
		}	
	 }
	 
	 IEnumerator LightFadeIn()
	 {
	 for( float l = 0.5f ; l <= 1f; l +=0.1f)
		{	
			this.c.a = l;
			this.rend.material.color = c;
		    yield return new WaitForSeconds(0.1f);
		}
		col.enabled = true;
	 }
}
