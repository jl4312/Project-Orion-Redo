using UnityEngine;
using System.Collections;

public class LevelPortal : MonoBehaviour {

	public bool[] isEnter;
	private GameObject[] playerList;
	//public GameObject myCamera;

	public bool ready;
	// Use this for initialization
	void Start () {
		ready = false;
		playerList = GameObject.FindGameObjectsWithTag("Player");
		isEnter = new bool[playerList.Length];

		for (int i = 0; i < playerList.Length; i++)
			isEnter [i] = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		ready = true;
		for (int i = 0; i < isEnter.Length; i++)
		{
			if (isEnter [i]) {
			}
			else{
				ready = false;
				break;
			}
		}


		if (this.GetComponent<TriggerScript> ().triggered) {
			if(ready)
			{
				PlayTransition();
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{

			for(int i = 0; i < playerList.Length; i++)
			{
				if(col.gameObject == playerList[i])
				{
					isEnter[i] = true;
				}
			}
		
	}

	void OnTriggerExit(Collider col)
	{
		for (int i = 0; i < playerList.Length; i++)
			if (playerList [i] == col.gameObject)
				isEnter [i] = false;
	}

	void PlayTransition()
	{
		Application.LoadLevel(Application.loadedLevel + 1);
		//Application.LoadLevel(
	}
}
/*

	// Update is called once per frame
    void Update()
    {
        if(player1 && player2)
        {
            movementAmount += 0.1f;
            transform.Rotate(0f,Time.deltaTime * 90f * (movementAmount * 1.1f),0f, Space.Self);
            transform.Translate(Vector3.up * movementAmount/30 * Time.deltaTime);

            if (movementAmount >= 50)
            {
                Application.LoadLevel("EndMenu");
            }
        }
        // checks if the player has just entered, as that way our input check for getting out doesn't immediatly assume the player's input to enter also indicated them exiting
        if (p1justEntered == true)
        {
            p1justEntered = false;
        }
        else
        {
            // if the player pressed square to get out and the rock is on the ground then we put the player on top of the block and turn its renderers and colliders on
            if (player1 && Input.GetButtonDown(player1.GetComponent<PlayerScript>().mySButton) && !player2)
            {
                //play unmeld
                soundSource.PlayOneShot(unmeld, 1.0f);

                player1.transform.position = transform.position + transform.up;
                player1.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player1.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
                player1.transform.GetChild(2).GetComponent<Animation>().enabled = true;
                player1.transform.GetComponent<Collider>().enabled = true;

                myCamera.GetComponent<CameraScript>().player1 = player1;
 

                player1 = null;
            }
        }

        //P2
        // checks if the player has just entered, as that way our input check for getting out doesn't immediatly assume the player's input to enter also indicated them exiting
        if (p2justEntered == true)
        {
            p2justEntered = false;
        }
        else
        {
            // if the player pressed square to get out and the rock is on the ground then we put the player on top of the block and turn its renderers and colliders on
            if (player2 && Input.GetButtonDown(player2.GetComponent<PlayerScript>().mySButton) && !player1)
            {
                //play unmeld
                soundSource.PlayOneShot(unmeld, 1.0f);

                player2.transform.position = transform.position + transform.up;
                player2.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player2.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
                player2.transform.GetChild(2).GetComponent<Animation>().enabled = true;
                player2.transform.GetComponent<Collider>().enabled = true;

                myCamera.GetComponent<CameraScript>().player2 = player2;

                player2 = null;
            }
        }

}*/
