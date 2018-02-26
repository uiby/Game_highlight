using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanAnimator : MonoBehaviour {
    Animator animator;

	void Awake () {
		animator = GetComponent<Animator>();
	}

    public void PlayRun() {
        animator.SetInteger("State", 2);
    }

    public void PlayWalk() {
        animator.SetInteger("State", 1);
    }

    public void PlayIdle() {
        animator.SetInteger("State", 0);
    }

    public void PlayCatchPlayer() {
        animator.SetInteger("State", 4);
    }
    public void PlayEscaped() {
        animator.SetInteger("State", 5);
    }
    
    public void PlayTureEnd() {
        animator.SetInteger("State", 6);
    }
}
