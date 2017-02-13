using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {
	public bool immortal;							//Immortal mode for testing purposes, Enabled in inspector
	static ScoreManager instance = null;
	private LevelManager levelManager;
	private int score = 0;
	private int lives = 2;							//StartLives
	private string[] balls = new string[10];
	//public Sprite[] ballSprites = new Sprite[2];
	//private Sprite[] specialBalls = new Sprite[10];


	void Awake(){
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}

	void Start(){
		levelManager = GameObject.FindObjectOfType<LevelManager> ();

		//initializes all balls to be of type "normal"
		for (int i = 0; i < balls.Length; i++) {
			balls [i] = "normal";
		}
		
	}

	void Update(){
		//Resets the score and lives after won/lost game
		if (SceneManager.GetActiveScene ().name.Equals ("Menu")) {
			score = 0;
			lives = 2;
		}
	}

	public void IncreaseScore(int points){
		score = score + points;
	}

	public void gainLife(){
		lives++;
	}

	public void loseLife(){
		if (levelManager == null) {
			levelManager = GameObject.FindObjectOfType<LevelManager> ();
		}

		lives--;
		if (lives < 0) {
			if (!immortal) {
				levelManager.ChangeLevel ("Score");
			}
		}
	}


		
	public void NewNotNormalBall(string type){
		bool ballTypeSwapped = false;
		for (int i = 0; i < balls.Length; i++){
			if (balls[i].Equals("normal") && ballTypeSwapped == false){
				balls [i] = type;
				ballTypeSwapped = true;
			}
		}
	}

	public string[] GetBalls(){
		return balls;
	}

	public int GetLives(){
		return lives;
	}

	public int GetScore(){
		return score;
	}

	public string GetNextBall(){
		return balls [0];
	}
}
