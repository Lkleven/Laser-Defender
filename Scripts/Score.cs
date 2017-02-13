using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text text = GetComponent<Text>();
		text.text = "Score: " + ScoreKeeper.score.ToString().PadLeft (6, '0');
		ScoreKeeper.ResetScore ();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
