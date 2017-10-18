using UnityEngine;
using System.Collections;

public class KillPlane : MonoBehaviour {

	public GameObject[] gameObjectList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		{
		
			for (int i =0; i < gameObjectList.Length; i++) {

				if (col.gameObject.name.Contains(gameObjectList [i].name))
					Destroy (col.gameObject);
			}
		}
	}
}
