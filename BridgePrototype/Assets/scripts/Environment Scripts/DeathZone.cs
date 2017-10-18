using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {

	private GameObject[] playerList;
	// Use this for initialization
	void Start () {
		playerList = GameObject.FindGameObjectsWithTag("Player");
	}
	

	public void OnTriggerEnter(Collider col)
	{
		if(col.tag == "SingleControlRock")
		{	
			if(col.GetComponent<SingleControlRock>().player1)
				col.GetComponent<SingleControlRock>().removePlayer();
			col.transform.position = col.GetComponent<SingleControlRock>().startPosition;
		}
		// if both players pass through the trigger (in player or vehicle form) then we update the rspawn locations
		if (col.tag == "Player" || (col.tag == "SingleControlRock" && col.GetComponent<SingleControlRock> ().player1)) {
			for(int i = 0; i < playerList.Length; i++)
			{
				//playerList[i].GetComponent<PlayerScript>().respawnPosition.x += i * 1f;
				playerList[i].GetComponent<PlayerScript>().Respawn(new Vector3(0,0, i * 1f));
				playerList[i].GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
			}

		}


	}
}
