using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucio : MonoBehaviour {

	float speedBoostTime = 5f;
	float speedBoostAmount = 2f;

	float preSpeedBoost;
	float speedBoostTimer;

	bool speedBoostActive = false;

	Player player;

	void Start () {
		player = GetComponent<Player> ();
	}
	
	void Update () {

		//Shift
		if (Input.GetKeyDown (KeyCode.LeftShift) && !speedBoostActive) {
			Shift (true);
		}

		if (speedBoostActive) {
			if (speedBoostTimer > 0) {
				speedBoostTimer -= Time.deltaTime;
			} else {
				Shift (false);
			}
		}

		//RMB

		if (Input.GetKeyDown(KeyCode.Mouse1)) {
			RMB();
		}
	}
		

	void Shift (bool activate) {
		if (activate) {
			Debug.Log ("Shift");
			speedBoostActive = true;
			speedBoostTimer = speedBoostTime;
			preSpeedBoost = GetComponent<Player> ().moveSpeed;
			player.moveSpeed *= speedBoostAmount;
		} else {
			Debug.Log ("UnShift");
			speedBoostActive = false;
			speedBoostTimer = 1;
			player.moveSpeed = preSpeedBoost;
		}
	}

	void RMB () {
		Debug.Log ("RMB");
	}

}

	