using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProvider : MonoBehaviour {
    CharacterMover mover;
    CharacterInput input;
    CharacterLight light;
    CharacterAnimator animator;

    [HideInInspector]
    public Rigidbody2D m_rigidbody2D;

	// Use this for initialization
	void Awake () {
		mover = GetComponent<CharacterMover>();
        input = GetComponent<CharacterInput>();
        light = GetComponent<CharacterLight>();
        animator = GetComponent<CharacterAnimator>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_rigidbody2D.gravityScale = 3.5f;
	}

    //Action
    public void Move(float move, bool jump) {
        mover.Move(move, jump);
    }
    public void MoveStop() {
        mover.Stop();
    }

    public IEnumerator LookLeft() {
        mover.Move(-0.1f, false);
        yield return null;
        mover.Move(0, false);
        yield return null;
    }
    public IEnumerator LookRight() {
        mover.Move(0.1f, false);
        yield return null;
        mover.Move(0, false);
        yield return null;
    }

    public void Shake() {
        mover.Shake();
    }

    public void BeginJump() {
        mover.BeginJump();
    }

    public bool IsJumping() {
        return mover.jumping;
    }

    public void ChangeDirection() {
        light.ChangeDirection();
    }

    //Animation
    public void Running() {
        animator.Run();
    }
    public void Stopping() {
        animator.Idle();
    }
    public void EndJump() {
        animator.EndJump();
    }
    public void PlayGameOver() {
        animator.GameOver();
    }
    public void PlayGameClear() {
        animator.GameClear();
    }
    public void PlayTureEnd() {
        animator.TureEnd();
    }
    public void PlaySecondEnd() {
        animator.SecondEnd();
    }

    //LIGHT
    public void LightOn() {
        light.On();
    }
    public void LightOff() {
        light.Off();
    }

    public void OnFrashLight() {
        light.OnFrashLight();
    }
}
