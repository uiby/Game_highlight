using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanLight : MonoBehaviour {
    [SerializeField] Transform frashLightTrans;
    Light frashLight;

	// Use this for initialization
	void Awake () {
        frashLight = frashLightTrans.GetComponent<Light>();
        frashLight.enabled = false;
    }

    public void On() {
        frashLight.enabled = true;
    }
}
