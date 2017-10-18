using UnityEngine;
using System.Collections;

public class CollectableTrigger : MonoBehaviour {


	public Collectable[] collectableList;
	private bool trigger = false;
	
	// Update is called once per frame
	void Update () {

		if (!trigger) {
			trigger = true;
			for (int i = 0; i < collectableList.Length; i++) {
				if(!collectableList[i].atPos || !collectableList[i].activated)
				{
					trigger = false;
					break;
				}

			}
		}else
			this.GetComponent<TriggerScript>().triggered = true;
			
	}


}
