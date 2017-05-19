using UnityEngine;
using System.Collections;


[RequireComponent(typeof (PlayerController))]
[RequireComponent(typeof (GunController))]
public class Player : LivingEntity {

	public float moveSpeed = 5;
	public float jumpSpeed = 500;
	public float gravity;
	public string abilityScript;

	float distToGround;

	Camera viewCamera;
	PlayerController controller;
	GunController gunController;

	protected override void Start () {
		base.Start ();
		controller = GetComponent<PlayerController> ();
		gunController = GetComponent<GunController> ();
		viewCamera = Camera.main;
		distToGround = GetComponent<BoxCollider> ().bounds.extents.y;
		Debug.Log (distToGround);

		UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent (gameObject, "Assets/Scripts/Player.cs (28,3)", abilityScript);
	}
	
	void Update () {
		if (!controller.isLocalPlayer) {
			return;
		} 

		//Movement Input
		Vector3 moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		Vector3 moveVelocity = moveInput.normalized * moveSpeed;
		controller.Move (moveVelocity);

		if (Input.GetKeyDown (KeyCode.Space) && IsGrounded()) {
			Debug.Log ("jump u prick");
			GetComponent<Rigidbody> ().AddForce (new Vector3 (0, jumpSpeed, 0), ForceMode.Impulse);
		}

		GetComponent<Rigidbody> ().AddForce (Vector3.down * gravity);

		//Look Input
		Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
		float rayDistance;

		if (groundPlane.Raycast (ray, out rayDistance)) {
			Vector3 point = ray.GetPoint (rayDistance);
			//Debug.DrawLine (ray.origin, point, Color.red);
			controller.LookAt(point);
		}

		//Weapon Input
		if (Input.GetMouseButton (0)) {
			gunController.CmdShoot ();
		}
	}

	bool IsGrounded () {
		return Physics.Raycast (transform.position, Vector3.down, distToGround + 1);
	}
}
