using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float damage = 100f;
	private GameObject player, enemy;

	void Start(){

	}

	public float GetDamage(){
		return damage;
	}

	public void Hit(){
		Destroy (gameObject);
	}
}
