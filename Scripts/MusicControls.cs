using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicControls : MonoBehaviour {
	static MusicControls instance = null;

	void Awake(){
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}
		
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name.Equals ("Menu")) {
			this.GetComponent<Canvas> ().enabled = true;
		} else {
			this.GetComponent<Canvas> ().enabled = false;
		}
	}
}
