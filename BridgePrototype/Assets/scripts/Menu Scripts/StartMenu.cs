using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	public GameObject[] textList;
	public GameObject cursor;
	public GameObject[] silo;

	public TextMesh lvText;

	private int index;
	private int level;

	public float timeDelay;
	private int numLevels = 2;
	private float counter;
	
	private bool[] playerActive;

	public GameObject[] particles;


	// Use this for initialization
	void Start () {

		Screen.SetResolution (1920, 1080, true);
		index =  0;
		counter = 0;
		level = 1;
		playerActive = new bool[silo.Length];
		resetSelect ();
		numLevels = Application.levelCount - 1;
		textList [index].GetComponent<TextMesh> ().color = new Color(1f,1f,1f, 1f );
		for (var i = 0; i < silo.Length; i++)
			silo [i].SetActive (true);

		cursor.transform.position = new Vector3(cursor.transform.position.x, 
		                                        textList[index].transform.position.y, 
		                                        cursor.transform.position.z);

		for (int i = 0; i < textList.Length; i++) {
			textList[i].transform.position = new Vector3(cursor.transform.position.x + .5f,
			                                             textList[i].transform.position.y,
			                                             cursor.transform.position.z);
		}


		/*
		if (Player1)
		{
			myOButton = "P1O";
			mySButton = "P1S";
			myTButton = "P1T";
			myXButton = "P1X";
			myR2Trigger = "P1R2";
			myL2Trigger = "P1L2";
			myLeftStick = "P1LeftStick";
			myRightStick = "P1RightStick";
			myShareButton = "P1Share";
		} else
		{
			myOButton = "P2O";
			mySButton = "P2S";
			myTButton = "P2T";
			myXButton = "P2X";
			myR2Trigger = "P2R2";
			myL2Trigger = "P2L2";
			myLeftStick = "P2LeftStick";
			myRightStick = "P2RightStick";
			myShareButton = "P2Share";
		}*/

        //P1 = this.gameObject.transform.GetChild(0).gameObject;
        //P2 = this.gameObject.transform.GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		cursor.transform.position = new Vector3(cursor.transform.position.x, 
		                                        textList[index].transform.position.y, 
		                                        cursor.transform.position.z);
		


		CharFade ();

		if(counter <= timeDelay )
			counter += Time.fixedDeltaTime;


		ToggleMenu();
		if (index == 1) {
			ToggleLevels();
		}

		if (Input.GetButtonDown ("P1X")|| Input.GetButtonDown("P2T")) {
			playerActive [0] = true;
			particles [0].SetActive (true);
		}
		if (Input.GetButtonDown ("P2X") || Input.GetButtonDown("P1T")) {
			playerActive [1] = true;
			particles [1].SetActive (true);
		}
				
		if (allActive ()) {
			switch(index)
			{
			case 0:
				Application.LoadLevel(1);
				break;
			case 1: 
				Application.LoadLevel (level);
				break;
			default:
				break;
			}
		}


			/*
		if((Input.GetButtonDown("P1X") || Input.GetButtonDown("P1S") || Input.GetButtonDown("P1T") || Input.GetButtonDown("P1O") || Input.GetButtonDown("P2X") || Input.GetButtonDown("P2S") || Input.GetButtonDown("P2T") || Input.GetButtonDown("P2O")))
        {
            Application.LoadLevel(1);
            Debug.Log("Loading scene!");
        }*/
	}

	bool allActive()
	{
		for (int i =0; i < playerActive.Length; i++)
			if (!playerActive[i] || silo[i].GetComponent<SpriteRenderer>().material.color.a < 1.0f)
				return false;
		return true;
	}
	public void LoadScene(int level)
	{
		Application.LoadLevel (level);
	}

	void resetSelect()
	{
		for (int i = 0; i < playerActive.Length; i++) {
			playerActive [i] = false;
			particles[i].SetActive(false);
		}	
	}

	void ToggleMenu()
	{
		if (counter < timeDelay)
			return;
		if (-1 * Input.GetAxis ("P1LeftStick" + "Y") > .95 || -1 * Input.GetAxis ("P2LeftStick" + "Y") > .95) {
			index--;
			textList [index + 1].GetComponent<TextMesh> ().color = new Color(.5f,.5f,.5f, .5f );

			counter = 0;
			resetSelect();
			if(index < 0)
				index = textList.Length - 1;
			textList [index].GetComponent<TextMesh> ().color = new Color(1f,1f,1f, 1f );
		}
		if (-1 * Input.GetAxis ("P1LeftStick" + "Y") < -.95 || -1 * Input.GetAxis ("P2LeftStick" + "Y") < -.95) {
			index++;
			textList [index - 1].GetComponent<TextMesh> ().color = new Color(.5f,.5f,.5f, .5f );

			counter = 0;
			resetSelect();
			index = index % textList.Length;
			textList [index].GetComponent<TextMesh> ().color = new Color(1f,1f,1f, 1f );
		}
	}
	void ToggleLevels()
	{
		if (counter < timeDelay)
			return;
		if(1 * Input.GetAxis ("P1LeftStick" + "X") < -.95 || 1 * Input.GetAxis ("P2LeftStick" + "X") < -.95)
		{
			level--;
			counter = 0;
			resetSelect();
			if(level < 1)
				level = numLevels;	 
			lvText.text = level + "";
		}
		if(1 * Input.GetAxis ("P1LeftStick" + "X") > .95 || 1 * Input.GetAxis ("P2LeftStick" + "X") > .95)
		{
			level++;
			counter = 0;
			resetSelect();
			if(level > numLevels)
				level = 1;
			lvText.text = level + "";
		}
	}

	void CharFade()
	{
		Color tmp;
		for (int i = 0; i < playerActive.Length; i++) {
			tmp = silo [i].GetComponent<SpriteRenderer> ().material.color; 

			if (playerActive [i] && silo [i].GetComponent<SpriteRenderer> ().material.color.a < 1)
			{
				tmp.a += .05f;
				silo [i].GetComponent<SpriteRenderer> ().material.color = tmp;
			}
			if (!playerActive [i] && silo [i].GetComponent<SpriteRenderer> ().material.color.a > 0)
			{
				tmp.a -= .05f;
				silo [i].GetComponent<SpriteRenderer> ().material.color = tmp;
			}

		}
	}
}
