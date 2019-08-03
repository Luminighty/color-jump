using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


	private Vector3 respawnPoint;
	private int deathCount = 0;

	private void Start() {
		respawnPoint = transform.position;
	}

	public void SetRespawnPoint() {respawnPoint = transform.position;}

	public void Respawn() {
		deathCount++;
		transform.position = respawnPoint;
	}

}
