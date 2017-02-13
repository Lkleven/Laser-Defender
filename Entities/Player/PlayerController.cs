using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float moveSpeed, tilt, padding, shotDelay, health, projectileSpeed, shieldCooldown = 5.0f;
	public GameObject projectile;
	public AudioClip laserSmall;

	private Transform[] guns;
	private GameObject engines;
	private float xMin, xMax, nextFire, chargeTimer = 0;
	private HealthUI healthUI;
	private ShipSelector shipSelector;
	private LevelManager levelManager;
	private string shipType;
	private GameObject shield = null, recharge;
	private bool shieldUp = false;

	// Use this for initialization
	void Start () {
		shipType = gameObject.name.Replace("Player_", "");
		shipSelector = GameObject.Find ("ShipSelector").GetComponent<ShipSelector> ();

		if (!shipType.Equals (shipSelector.GetSelectedShip())) {
			Destroy (gameObject);
			return;
		}

		if (transform.Find ("Shield") != null) {
			shieldUp = true;
			shield = transform.Find ("Shield").gameObject;
			recharge = transform.Find ("Recharge").gameObject;
		}

		guns = transform.Find ("Guns").gameObject.GetComponentsInChildren<Transform> ();
		healthUI = GameObject.Find ("HealthUI").GetComponent<HealthUI> ();
		healthUI.SetFullHPValue (health);
		healthUI.SetShipType (shipType);
		healthUI.UpdateHealth (health);

		engines = transform.Find ("EngineFull").gameObject;
		engines.SetActive (false);
		
		float distance = transform.position.z - Camera.main.transform.position.z; //distance between player and camera
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftmost.x + padding;
		xMax = rightmost.x - padding;
	}
		
	
	// Update is called once per frame
	void Update(){
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//transform.position += new Vector3 (-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * moveSpeed * Time.deltaTime;
			engines.SetActive (true);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			//transform.position += new Vector3 (+speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * moveSpeed * Time.deltaTime;
			engines.SetActive (true);
		} else {
			engines.SetActive (false);
		}

		//clamps the movement on the X-axis to xMin and xMax
		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

		if (Input.GetKey ("space") && Time.time > nextFire || Input.GetMouseButton(0) && Time.time > nextFire) {
			Fire ();
		}

		if (shield && shieldUp == false) {
			chargeTimer += Time.deltaTime;
			Vector3 temp = recharge.GetComponent<Transform> ().localScale;
			float fullCharge = 2.8f;
			temp.x = fullCharge/5 * chargeTimer;
			recharge.GetComponent<Transform> ().localScale = temp;
			Debug.Log (temp.x);
			shieldCooldown -= Time.deltaTime;
			if (shieldCooldown < 0) {
				ShieldCharged ();
			}
		}
	}

	void OnTriggerEnter (Collider collider) {
		Projectile laserBolt = collider.gameObject.GetComponent<Projectile> ();
		if (laserBolt && !shieldUp ) {
			health -= laserBolt.GetDamage ();
			laserBolt.Hit ();
			healthUI.UpdateHealth (health);
			if (health <= 0) {
				PlayerDeath ();
			}
		} else if (shield) {
			ShieldAbsorb ();
			Destroy (collider); //Destroy laser before it hits the ship
		}
	}

	void PlayerDeath(){
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		Destroy(gameObject);
		levelManager.ChangeLevel ("Score");
	}

	void Fire(){
		for(int i = 1; i < guns.Length; i++){
			Vector3 shotSpawn = guns[i].position + new Vector3 (0, 0.15f, 0);
			nextFire = Time.time + shotDelay;
			GameObject shot = Instantiate (projectile, shotSpawn, Quaternion.identity) as GameObject;
			shot.GetComponent<Rigidbody> ().velocity = transform.up * projectileSpeed;
			AudioSource.PlayClipAtPoint (laserSmall, transform.position);
		}
	}

	void ShieldAbsorb(){
		Vector3 temp = recharge.GetComponent<Transform> ().localScale;
		temp.x = 0;
		recharge.GetComponent<Transform> ().localScale = temp;

		shieldUp = false;
		shield.SetActive (false);
		chargeTimer = 0;
	}

	void ShieldCharged(){
		shieldUp = true;
		shield.SetActive (true);
		shieldCooldown = 5.0f;
	}

}

