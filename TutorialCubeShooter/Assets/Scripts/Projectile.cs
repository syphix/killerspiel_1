using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float deathTime;
	float speed = 15;
	float damage = 1;

	public void setSpeed (float newSpeed) {
		speed = newSpeed;
	}

	void Start () {
		Destroy (gameObject, deathTime);
		GetComponent<Rigidbody> ().velocity = transform.forward * 15;
	}
	
	void Update () {
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance);
	}

	void CheckCollisions(float moveDistance) {
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, moveDistance)) {
			OnHitObject (hit);
		}
	}

	void OnHitObject(RaycastHit hit) {
		IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
		if (damageableObject != null) {
			damageableObject.TakeHit (damage, hit);
		}
		GameObject.Destroy (gameObject);
	}
}
