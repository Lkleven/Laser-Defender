using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/*
*Attached to UI-BottomUI-LivesPanel
*Keeps track of and displays the correct amount and different types of remaining lives (balls)
*/
public class Lives : MonoBehaviour {
	private ScoreManager scoreManager;
	private int extraLives;								//extra balls
	private GameObject[] lives = new GameObject[10];	//Lives are stored in a GameObject array. Each GameObject containing  an image displaying the ball-type. Max 10 extra lives.
	private string[] ballTypes;							//An array containing all the balls currently in a players posession

	//private Ball ball;
	private int prevLives = 0;
	private Color ballColor;

	//Sprites for different balltypes, assigned in the inspector
	public Sprite normalBall;
	public Sprite lightningBall;

	// Use this for initialization
	void Start () {
		//ball = GameObject.FindObjectOfType<Ball> ();
		//ballColor = ball.GetComponent<SpriteRenderer>().color;

		for (int i = 0; i < lives.Length; i++) {
			lives [i] = GameObject.Find ("Life" + (i + 1));
		}

		scoreManager = GameObject.FindObjectOfType<ScoreManager> ();
	}

	// Update is called once per frame
	void Update () {
		extraLives = scoreManager.GetLives ();
		ballTypes = scoreManager.GetBalls ();

		if (prevLives > extraLives) {
			UpdateLives ();
		}
		prevLives = extraLives;


		showLives ();
	}

	void showLives(){
		for (int i = 0; i < lives.Length; i++) {
			Image image = lives [i].GetComponent<Image> ();
			Color color = image.color;

			if (i < extraLives) {
				color.a = 1;
				image.color = color;
				if (ballTypes[i].Equals("lightning")){
					image.sprite = lightningBall;
					image.color = Color.white;
				} else {
					image.sprite = normalBall;
					image.color = ballColor;
				}
			} else {
				color.a = 0;
				image.color = color;
			}
		}
	}

	void UpdateLives(){
		for (int i = 0; i < lives.Length -1; i++) {
			Image image = lives [i].GetComponent<Image> ();
			image.sprite = lives [i + 1].GetComponent<Image> ().sprite;
			ballTypes [i] = ballTypes [i + 1];
		}
	}


	//Debugging
	/*void PrintAllBallSprites(){
		for (int i = 0; i < lives.Length - 1; i++) {
			Debug.Log (i + ":: " + lives [i].GetComponent<Image> ().sprite);
		}
	}*/
}
