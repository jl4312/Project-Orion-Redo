using UnityEngine;
using System.Collections;

public class ColorScript : MonoBehaviour
{
	
	// the boolean
	public bool isWhite;
	public bool change = false;

	private CameraScript cam;
	private Renderer[] rS;
	private float alpha = 1;
	private bool collide = false;

	void Start()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
		rS = transform.GetComponentsInChildren<Renderer>();

		if(isWhite)
			rS[1].material.color = new Color(1, 1, 1, 1f);
		else
			rS[1].material.color = new Color(0, 0, 0, 1f);

		ChangeCollision ();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{

		if (rS [1].material.color.a < 1f) {
			if (!collide)
				rS [1].material.color = new Color (rS [1].material.color.r, 
			                                 rS [1].material.color.r,
			                                 rS [1].material.color.r, 
			                                 rS [1].material.color.a + .05f);
			rS[0].enabled = false;
		}
		else{
			rS[0].material.color = rS[1].material.color;
			rS[0].enabled = true;

			ChangeCollision();
		}

	
	}
	
	// function used to return a boolean to players when they need to know if they match the object of the attached object
	public bool IsMyColor(bool player1)
	{
		return (player1 != isWhite);
	}

	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Player") {
			if (change) {
				isWhite = !isWhite;
				if (isWhite)
					rS [1].material.color = new Color (1, 1, 1, 1f);
				else
					rS [1].material.color = new Color (0, 0, 0, 1f);
			}
			collide = false;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Player" && rS[1].material.color.a < .1f) {
			rS[1].material.color = new Color(rS[1].material.color.r, 
			                                 rS[1].material.color.r,
			                                 rS[1].material.color.r, 0f);
			collide = false;
		}
	}

	void OnTriggerStay(Collider col)
	{

		if (col.gameObject.tag == "Player") {
			if((isWhite && !col.gameObject.GetComponent<PlayerScript>().Player1) ||
			   (!isWhite && col.gameObject.GetComponent<PlayerScript>().Player1))
			{
				if(rS[1].material.color.a > 0f)
					rS[1].material.color = new Color(rS[1].material.color.r, 
			                                 rS[1].material.color.r,
			                                 rS[1].material.color.r, 
					                         rS[1].material.color.a - .05f);
			}
			collide = true;
		}

	}
	void ChangeCollision()
	{
		if (this.gameObject.tag != "Harmful" || this.gameObject.tag != "SingleControlRock") {
			// depending on the color value of the object, we set its color and we set it to not collide with the player unless it is a harmful item or control rock
			if (isWhite) {
				
				Physics.IgnoreCollision (transform.GetComponent<Collider> (), cam.player2.GetComponent<Collider> ());
				Physics.IgnoreCollision (transform.GetComponent<Collider> (), cam.player1.GetComponent<Collider> (), false);
				
			} else {
				Physics.IgnoreCollision (transform.GetComponent<Collider> (), cam.player1.GetComponent<Collider> ());
				Physics.IgnoreCollision (transform.GetComponent<Collider> (), cam.player2.GetComponent<Collider> (), false);
				
			}
		}
	}

}
