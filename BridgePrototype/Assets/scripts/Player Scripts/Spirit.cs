using UnityEngine;
using System.Collections;

public class Spirit : MonoBehaviour {

	public GameObject[] playerList;

	private GameObject seeker;
	public GameObject particle;
	public GameObject explosion;

	public float collisionDist = 0;

	private int targetIndex = 0;

	private Vector3 velocity;
	private Vector3 acceleration;

	public float maxSpeed = 0;
	public float maxForce = 0;

	public float rotationSpeed = 1;


	private float angle;
	private bool hover = false;
	private float radius = 1.5f;

	public float maxDist = 3f;

	public Color color = Color.white;
	private Vector3 target;
	// Use this for initialization
	void Start () {
		acceleration = new Vector3 (0, 0, 0);
		velocity = new Vector3 (0, 0, 0);
		seeker = (GameObject)Instantiate(particle, playerList [targetIndex].transform.position, playerList [targetIndex].transform.rotation);
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((playerList [0].transform.position - playerList [1].transform.position).magnitude < maxDist) {
			radius = (playerList [0].transform.position - playerList [1].transform.position).magnitude / 2 + maxDist / 2 ;
			Circle ((playerList [0].transform.position + playerList [1].transform.position) / 2);
			color.g = color.r = color.b = .5f;
		} else if (hover) {
			radius = 1.5f;
			Circle (playerList [targetIndex].transform.position);

			color.g = color.r = color.b = targetIndex;

			if (angle > Mathf.PI * 2 * 3 )
			{
				hover = false;
				targetIndex = (targetIndex + 1) % playerList.Length;
				angle = 0;
			}
		} else {
			target = playerList [targetIndex].transform.position;

			if(targetIndex > color.r)
				color.r += .005f;
			if(targetIndex < color.r)
				color.r -= .005f;

			color.g = color.b = color.r;
			//seeker.getGetComponentInChildren<ParticleSystem>().startColor = new Vector4 (targetIndex, targetIndex, targetIndex, 1);
			CheckCollision ();
		}

		seeker.GetComponent<ParticleSystem>().startColor = color;
		foreach (var child in seeker.GetComponentsInChildren<ParticleSystem>())
			child.startColor = color;
		Seek (target);

		velocity += acceleration;
		if (velocity.magnitude > maxSpeed) {
			velocity = velocity.normalized * maxSpeed;
		}
		seeker.transform.position += velocity / 10;
		acceleration *= 0;


	}

	void Seek(Vector3 target)
	{
		Vector3 tarDist = target - seeker.transform.position;
		tarDist = tarDist.normalized;
		tarDist *= maxSpeed;

		Vector3 steer = tarDist - velocity;

		if (steer.magnitude > maxForce) {
			steer = steer.normalized * maxForce;
		}
		acceleration += steer;
	}

	void CheckCollision(){
		if ((playerList [targetIndex].transform.position - seeker.transform.position).magnitude < collisionDist) {
			GameObject tmp = (GameObject)Instantiate(explosion, playerList [targetIndex].transform.position, playerList [targetIndex].transform.rotation);
			foreach (var child in tmp.GetComponentsInChildren<ParticleSystem>())
				child.startColor = color;
			Destroy (tmp, 5);
			hover = true;

		}
	}

	void Circle(Vector3 center)
	{
		angle += rotationSpeed * Time.deltaTime;

		var offset = new Vector3 (Mathf.Sin (angle) * radius, center.y - 1f, Mathf.Cos (angle) * radius);
		target = center + offset;
	}
}
