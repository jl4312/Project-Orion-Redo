using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetShooter : MonoBehaviour {

	// the reference to the prefab being used to spawn new balls
	public GameObject rockPrefab;
	public GameObject shooter;

	// the list of balls fired by the cannon
	public List<GameObject> Rocks;
	
	// the maximum point the shots can go
	public GameObject MaxDistPoint;
	
	// the spawn point for new balls
	public GameObject RockStartPoint;
	
	// used to set the velocity of the projectiles
	public float rockSpeed;
	
	// scalar used to represent magnitude from maxdistpoint to the shooter position
	private float distance;
	
	// a list containing values for how far each projectile has gone
	public List<float> distGone;
	
	// values used to track the timer that informs when projectiles a created and fired
	public float totalShotTime;
	public float shotTimeOffset;
	private float currentShotTime;
	private float startShotTime;

	public GameObject[] playerList;

	public float searchDist = 0;

	GameObject tmp;
	private GameObject target;
	void Start(){
		// setting initial distance and shot time
		distance = (transform.position - MaxDistPoint.transform.position).magnitude;
		startShotTime = Time.time - shotTimeOffset;
		// setting values for speed and shottime if designers have not
		if (rockSpeed == 0) {
			rockSpeed =1;
		}
		if (totalShotTime == 0) {
			totalShotTime =3;
		}
		
		// setting up the two lists for tracking projectiles and their distances
		Rocks = new List<GameObject> ();
		distGone = new List<float> ();

		playerList = GameObject.FindGameObjectsWithTag("Player");
		target = playerList [0];
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < playerList.Length; i++) {
			tmp = playerList [i];
			if (playerList [i].GetComponent<PlayerScript> ().meldObject )
			{
				tmp = playerList [i].GetComponent<PlayerScript> ().meldObject;
			
			}

			if ((tmp.transform.position - this.transform.position).magnitude < (target.transform.position - this.transform.position).magnitude) {
				target = tmp;
			}
		}

		currentShotTime = Time.time - startShotTime;

		if (target && (target.transform.position - this.transform.position).magnitude < searchDist) {
			Aim (target);
			if (currentShotTime >= totalShotTime)
				Fire ();
		}


	}
	
	// function that destroys a projectile, then removes its info from both lists
	public void DestroyBall(int i){
		Rocks.RemoveAt(i);
		GameObject.Destroy(Rocks[i]);
		distGone.RemoveAt(i);
	}
	
	// function that fires a new projectile, which means creating a new ball and setting all its values accordingly
	public void Fire()
	{
		startShotTime= Time.time;
		Rocks.Add(GameObject.Instantiate(rockPrefab));
		Rocks[Rocks.Count-1].transform.position = RockStartPoint.transform.position;
		Rocks[Rocks.Count-1].GetComponent<BallScript>().velo = shooter.transform.up * .03f * rockSpeed;
		Rocks[Rocks.Count-1].transform.parent = transform;
		Rocks[Rocks.Count-1].GetComponent<BallScript>().ballNum = Rocks.Count-1;

		if (this.GetComponent<ColorScript> ()) {
			Rocks[Rocks.Count-1].AddComponent<ColorScript>();
			Rocks[Rocks.Count-1].GetComponent<ColorScript>().isWhite = this.GetComponent<ColorScript> ().isWhite;
		}


		//distGone.Add(0);
	}

	public void Aim (GameObject target)
	{

		if (target) {
			//Vector3 tmp = new Vector3(target.transform.position.x + 90, );
			shooter.transform.LookAt (target.transform.position);
			shooter.transform.Rotate (new Vector3 (90, 15, 0));
			//this.transform.rotation = 
		}
	}
}