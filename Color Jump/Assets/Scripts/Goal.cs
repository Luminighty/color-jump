using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
	[SerializeField]
	float winTime;

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			Debug.Log("Win");
			StartCoroutine(Win());
		}
	}

	IEnumerator Win() {
		PhysicsObject.isPhysicsOn = false;
		yield return new WaitForSeconds(winTime);
		PhysicsObject.isPhysicsOn = false;
	
		SceneManager.LoadScene("MainMenu");
	}
}
