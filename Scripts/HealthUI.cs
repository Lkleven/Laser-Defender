using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthUI : MonoBehaviour {
	public AudioClip warningSound;

	private GameObject healthBar, criticalText;
	private Text healthText;
	private float fullHPValue = 100, currentHP;
	private Color fullHPColor;
	private PlayerController[] playerOptions;
	private PlayerController player;
	private string shipType;

	

	// Use this for initialization
	void Start () {
		healthBar = transform.Find ("HPBar").gameObject;
		healthText = transform.Find ("HPText").gameObject.GetComponent<Text> ();
		criticalText = transform.Find ("CriticalText").gameObject;

		if (GameObject.Find ("Player_Bertram") != null) {
			shipType = "Player_Bertram";
		} else if (GameObject.Find ("Player_Sphynx") != null) {
			shipType = "Player_Spyhnx";
		} else if (GameObject.Find ("Player_Tarkan") != null) {
			shipType = "Player_Tarkan";
		}
		player = GameObject.Find (shipType).GetComponent<PlayerController> ();

		fullHPColor = healthBar.GetComponent<Image> ().color;
		criticalText.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateHealth(float health){
		//To prevent negative values i.e. -50/250
		if (health < 0) {
			health = 0;
		}
		healthText.text = health + "/" + fullHPValue;
		UpdateHealthBar (health);
	}

	void UpdateHealthBar(float health){
		float healthPercentage = health / fullHPValue;
		Color color; 
		healthBar.GetComponent<Image> ().fillAmount = healthPercentage;

		if (healthPercentage > 0.75) {
			color = fullHPColor;
			CriticalHullWarning (false);
		} else if (healthPercentage <= 0.75 && healthPercentage > 0.25) {
			color = Color.yellow;
			CriticalHullWarning (false);
		} else {
			color = Color.red;
			if (healthPercentage > 0) {
				CriticalHullWarning (true);
			}
		}
		healthBar.GetComponent<Image> ().color = color;
	}

	private void CriticalHullWarning(bool show){
		if (show) {
			criticalText.SetActive (show);
			AudioSource.PlayClipAtPoint (warningSound, new Vector3(0,0,0));
			//AudioSource.PlayClipAtPoint (warningSound, player.gameObject.transform.position);
		}
	}

	public void SetFullHPValue(float healthValue){
		fullHPValue = healthValue;
	}

	public void SetShipType(string shipType){
		shipType = shipType;
	}
}
