using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject target;
	// Use this for initialization
	void LateUpdate()
	{
		transform.position = new Vector3 (target.transform.position.x,
		                                 10 + target.transform.position.y,
		                                 target.transform.position.z);
	}
}
