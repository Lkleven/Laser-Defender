using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	private Text text;
	public static int score = 0;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text> ();
	}


	public void Score(int points){
		score += points;
		text.text = "Score: " + (score.ToString ().PadLeft (8, '0'));
	}

	public static void ResetScore(){
		score = 0;
		//text.text = "Score: " + (score.ToString ().PadLeft (8, '0'));
	}

	public int GetScore(){
		return score;
	}
}
