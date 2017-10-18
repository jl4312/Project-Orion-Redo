using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

	public bool activated;
	public bool atPos;
	public GameObject endPos;
	public GameObject core;

	public float maxForce = 1.0f;
	public float maxSpeed = 3.0f;
	private Vector3 velocity;
	private Vector3 acceleration;

	private float collisionDist = 0.1f;
	// Use this for initialization
	void Start () {
		activated = false;
		atPos = false;

		acceleration = new Vector3 (0, 0, 0);
		velocity = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {

		if (activated && !atPos) {
			CheckCollision ();

			Arrive (endPos.transform.position);
			velocity += acceleration;
			if (velocity.magnitude > maxSpeed) {
				velocity = velocity.normalized * maxSpeed;
			}
			core.transform.position += velocity / 10;
			acceleration *= 0;


		}

	}
	void Arrive(Vector3 target)
	{
		Vector3 tarDist = target - core.transform.position;
		float d = tarDist.magnitude;
		tarDist = tarDist.normalized;

		if (d < 100) {
			float m = Mathf.Lerp (100, maxSpeed, d/100);
			tarDist *= m;
		}else
			tarDist *= maxSpeed;



		
		Vector3 steer = tarDist - velocity;
		
		if (steer.magnitude > maxForce) {
			steer = steer.normalized * maxForce;
		}
		acceleration += steer;
	}

	void CheckCollision(){

		if ((core.transform.position - endPos.transform.position).magnitude < collisionDist) {
			atPos = true;
		}
	}

	public void Activate()
	{
		activated = true;
	}


}
