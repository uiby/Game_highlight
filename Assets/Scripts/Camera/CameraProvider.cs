using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProvider : MonoBehaviour {
    CameraShaker cameraShaker;

	void Start () {
        cameraShaker = GetComponent<CameraShaker>();
	}

    public void Shake(float duration, float shakeRate) {
        cameraShaker.Shake(duration, shakeRate);
    }
}
