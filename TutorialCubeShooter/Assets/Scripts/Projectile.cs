using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public LayerMask collisionMask;
	public float deathTime;
	float speed = 10;
	float damage = 1;
	float deathTimer;

	public void setSpeed (float newSpeed) {
		speed = newSpeed;
	}

	void Start () {
		deathTimer = deathTime;
	}
	
	void Update () {
		if(deathTimer < 0) {
			GameObject.Destroy (gameObject);
		} else {
			deathTimer -= Time.deltaTime;
		}
		
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance);
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
	}

	void CheckCollisions(float moveDistance) {
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide)) {
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
