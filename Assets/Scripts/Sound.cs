using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour {
	static AudioSource audio;
	static int[] musicalScale = new int[]{0, 2, 4, 5, 7, 9, 11, 12};

	void Awake() {
		audio = GetComponent<AudioSource> ();
	}

	public static void PlaySE(AudioClip clip, int scale = 0) {
		audio.pitch = Mathf.Pow(2, musicalScale[scale] / 12f);
		audio.PlayOneShot (clip);
	}
}
