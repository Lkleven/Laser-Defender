using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float health = 150, shotsPerSecond = 0.5f, projectileSpeed;
	public GameObject projectile;
	public int scoreValue = 150;
	public AudioClip alienLaserSmall, alienExplosion;

	private ScoreKeeper scoreKeeper;
	private float startDelay = 1f, maxHealth; 		//Prevents aliens from shooting before they are in place (after animation complete)
	private bool inPosition = false;
	private GameObject healthBar;

	void Start(){
		maxHealth = health;
		healthBar = transform.Find ("HealthBar").gameObject;
		scoreKeeper = GameObject.Find ("ScoreText").GetComponent<ScoreKeeper> ();
		UpdateHealthBar ();
		healthBar.SetActive (false);
	}

	void Update(){
		startDelay -= Time.deltaTime;
		if (startDelay < 0) {
			inPosition = true;
		}

		if(inPosition){
			healthBar.SetActive (true);
			float probability = Time.deltaTime * shotsPerSecond; //probability needs to be a value between 0 and 1 as Random.value returns a value between 0 and 1
			if (Random.value < probability) {
				Fire ();
			}
		}
	}

	void Fire(){
		Vector3 shotSpawn = transform.position + new Vector3 (0, -0.5f, 0);
		GameObject shot = Instantiate (projectile, shotSpawn, Quaternion.identity) as GameObject;
		shot.GetComponent<Rigidbody> ().velocity = (-transform.up) * (-projectileSpeed);
		AudioSource.PlayClipAtPoint (alienLaserSmall, transform.position, 1.0f);
	}

	// Use this for initialization
	void OnTriggerEnter (Collider collider) {
		Projectile laserBolt = collider.gameObject.GetComponent<Projectile> ();
		if (laserBolt) {
			health -= laserBolt.GetDamage ();
			laserBolt.Hit ();
			scoreKeeper.Score (10);
			UpdateHealthBar ();
			if (health <= 0) {
				Dies ();
			}
		}
	}

	void Dies(){
		Destroy(gameObject);
		scoreKeeper.Score (scoreValue);
		AudioSource.PlayClipAtPoint (alienExplosion, transform.position, 5.0f);
	}

	void UpdateHealthBar(){
		Vector3 temp = healthBar.GetComponent<Transform> ().localScale;
		float healthPercentageRemaining = health / maxHealth;
		temp.x = temp.x * healthPercentageRemaining;

		healthBar.GetComponent<Transform> ().localScale = temp;
	}

}
