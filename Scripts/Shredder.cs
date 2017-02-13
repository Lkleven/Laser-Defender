using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider collider){
		Destroy (collider.gameObject);
	}


}
