using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
	[SerializeField]
	float winTime;

	AudioSource audioSource;

	private void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			Debug.Log("Win");
			StartCoroutine(Win());
		}
	}

	IEnumerator Win() {
		audioSource.Play();
		PhysicsObject.isPhysicsOn = false;
		yield return new WaitForSeconds(winTime);
		PhysicsObject.isPhysicsOn = true;
	
		SceneManager.LoadScene("MainMenu");
	}
}
