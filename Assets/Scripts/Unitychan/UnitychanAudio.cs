using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UnitychanAudio : MonoBehaviour {
    [SerializeField] AudioClip footStep;
    AudioSource audio;
    int[] musicalScale = new int[]{0, 2, 4, 5, 7, 9, 11, 12};

    void Awake() {
        audio = GetComponent<AudioSource> ();
    }

    public void PlayFootStep(int scale,float volume) {
        audio.volume = volume;
        PlaySE(footStep, scale);
    }

    void PlaySE(AudioClip clip, int scale = 0) {
        audio.pitch = Mathf.Pow(2, musicalScale[scale] / 12f);
        audio.PlayOneShot (clip);
    }
}
