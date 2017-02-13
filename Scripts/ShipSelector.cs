using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShipSelector : MonoBehaviour {
	static ShipSelector instance = null;

	public Sprite[] shipSprites = new Sprite[3];

	private Button previous, next;
	private Text ship, stats;
	private string[] availableShips = new string[]{ "Bertram", "Sphynx", "Tarkan" };
	//Equipment1, Equipment2, RateOfFire, Hit Points, Speed
	private string[] statistics = new string[]{ "Laser\nShield\n6\n250\n10",
												"Laser\nLaser\n4\n450\n13",
												"Laser\nLaser\n3.3\n800\n8"};
	private int selector;
	private bool started = false;
	private GameObject playerObject; 
	private static GameObject selectedShip;
	private Image shipImg;

	void Awake(){
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		selector = 0;
		previous = transform.Find ("Panel/Previous").gameObject.GetComponent<Button> ();
		next = transform.Find ("Panel/Next").gameObject.GetComponent<Button> ();
		ship = transform.Find ("Panel/Ship").gameObject.GetComponent<Text> ();
		stats = transform.Find ("Panel/Stats").gameObject.GetComponent<Text> ();
		shipImg = transform.Find ("Panel/Image").gameObject.GetComponent<Image> ();

		//Initialize
		ship.text = availableShips [selector];
		stats.text = statistics [selector];
		shipImg.sprite = shipSprites [selector];
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name.Equals ("Game") && !started) {
			started = true;
		}
		if (SceneManager.GetActiveScene().name.Equals ("Menu")) {
			this.GetComponent<Canvas> ().enabled = true;
		} else {
			this.GetComponent<Canvas> ().enabled = false;
		}
		if (SceneManager.GetActiveScene ().name.Equals ("Score")) {
			started = false;
		}
			
	}

	public void nextShip(){
		selector++;
		if (selector > availableShips.Length - 1) {
			selector = 0;
		}
		ship.text = availableShips [selector];
		stats.text = statistics [selector];
		shipImg.sprite = shipSprites [selector];
	}

	public void previousShip(){
		selector--;
		if (selector < 0) {
			selector = availableShips.Length-1;
		}
		ship.text = availableShips [selector];
		stats.text = statistics [selector];
		shipImg.sprite = shipSprites [selector];
	}

	public string GetSelectedShip(){
		return ship.text;
	}
}
