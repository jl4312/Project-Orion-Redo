using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour
{
	
	public Vector3 velo;
	public int ballNum;

	private float time;
	void Start()
	{
		time = 0;
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		// move the ball by its velocity and increment its shooter's reference to it's distance moved
		transform.position += velo;

		time += Time.deltaTime;

	
		if (time > 3 && this.gameObject)
			Destroy (this.gameObject);

	}
	
	void OnCollisionEnter(Collision col)
	{
		// if the ball hits objects specifically designed to stop them then we destroy the ball
		if (col.gameObject.tag == "Screw" || col.gameObject.tag == "Protected" || col.gameObject.tag == "Platform")
		{
			Destroy (this.gameObject);
			//transform.parent.GetComponent<RockShooterScript>().DestroyBall(ballNum);
		}

		if (col.gameObject.tag == "Projectile") {

			Vector3 tmp = velo;
			velo = col.gameObject.GetComponent<BallScript>().velo;
			col.gameObject.GetComponent<BallScript>().velo = tmp;
		}

		
		if (col.gameObject.tag == "SingleControlRock") {

			if (col.gameObject.GetComponent<SingleControlRock> ().player1) {

				GameObject player = col.gameObject.GetComponent<SingleControlRock> ().player1;
				//col.gameObject.GetComponent<Rigidbody>().AddForce((col.gameObject.transform.position - transform.position).normalized * 10f);
				col.gameObject.GetComponent<SingleControlRock> ().removePlayer ();

				//player.GetComponent<Rigidbody> ().AddForce ((col.gameObject.transform.position - transform.position).normalized * 10f);
			}
			col.gameObject.GetComponent<Rigidbody>().AddForce((col.gameObject.transform.position - transform.position).normalized * 4f);

			DestroyBall ();
		}
	}



	
	// this function calls a corresponding function in it's parent shooter to destroy it entirely
	public void DestroyBall()
	{
		Destroy (this.gameObject,.5f);
		//transform.parent.GetComponent<RockShooterScript>().DestroyBall(ballNum);
	}
}
