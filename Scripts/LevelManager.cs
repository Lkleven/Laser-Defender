using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	GameObject infoPanel;

	public void Start(){
		infoPanel = GameObject.Find("InfoPanel");
		if (infoPanel != null) {
			infoPanel.SetActive (false);
		}
	}

	public void Update(){
		//for testing purposes
		/*if (Input.GetKeyDown ("space")) {
			LoadNextLevel ();
		}*/
			
	}
	
	public void ChangeLevel(string levelToLoad){
		SceneManager.LoadScene (levelToLoad);
	}

	public void LoadNextLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex +1);
	}

	public void ToggleInfoScreen(bool showInfo){
			infoPanel.SetActive (showInfo);
	}

	public void Quit(){
		Debug.Log ("Quit");
		Application.Quit ();
	}
}
