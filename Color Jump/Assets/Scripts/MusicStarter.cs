using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStarter : MonoBehaviour
{
	[SerializeField]
	AudioSource source;
	[SerializeField]
	float delay;

	private void Awake() {
		source.PlayDelayed(delay);
	}

}
