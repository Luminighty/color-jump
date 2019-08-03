using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject DeathParticlePrefab;
	public GameObject WalkParticlePrefab;
	public GameObject LandParticlePrefab;
	public AudioClip jumpClip;
	public AudioClip deathClip;
	public AudioSource soundEffectSource;

	private Vector3 respawnPoint;
	private int deathCount = 0;

	private void Start() {
		respawnPoint = transform.position;
	}

	public void SetRespawnPoint() {respawnPoint = transform.position;}

	public void Respawn() {
		deathCount++;
		GameObject i = Instantiate(DeathParticlePrefab, transform.position, Quaternion.identity) as GameObject;
		ParticleSystem particle = i.GetComponent<ParticleSystem>();
		ParticleSystem.MainModule main = particle.main;
		main.startColor = GetComponentInChildren<SpriteRenderer>().color;
		transform.position = respawnPoint;
		soundEffectSource.PlayOneShot(deathClip);
	}

}
