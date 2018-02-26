using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanProvider : MonoBehaviour {
    UnitychanMover mover;
    UnitychanAnimator animator;
    UnitychanController controller;
    UnitychanAudio audio;
    UnitychanLight light;
    SpriteRenderer sprite;
    Collider2D collider;

	// Use this for initialization
	void Awake () {
		mover = GetComponent<UnitychanMover>();
        animator = GetComponent<UnitychanAnimator>();
        controller = GetComponent<UnitychanController>();
        audio = GetComponent<UnitychanAudio>();
        light = GetComponent<UnitychanLight>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        Hide();
	}

    public UnitychanState NowState() {
        return controller.NowState();
    }

    //Action
    public void Show() {
        sprite.enabled = true;
        collider.enabled = true;
    }
    public void Hide() {
        sprite.enabled = false;
        collider.enabled = false;
    }

    public void StartMove() {
        controller.StartMove();
        animator.PlayWalk();
    }
    public void StopMove() {
        controller.StopMove();
        animator.PlayIdle();
    }

    public void StartRun() {
        animator.PlayRun();
    }

    //animator
    public void PlayCatchPlayerAnimation() {
        animator.PlayCatchPlayer();
    }
    public void PlayEscapedAnimation() {
        animator.PlayEscaped();
    }

    public void PlayTureEndAnimation() {
        animator.PlayTureEnd();
        if (controller.IsLeftAtPlayerPos()) sprite.flipX = true;
    }

    public void LightOn() {
        light.On();
    }

    public float GetDistance() {
        return controller.GetDistance();
    }
    public float GetPercent() {
        return controller.GetPercent();
    }
    public Vector2 GetPos() {
        return (Vector2)transform.position;
    }

    //audio
    public void PlayFootStep(int scale, float volume) {
        audio.PlayFootStep(scale, volume);
    }

    public IEnumerator Drop() {
        var timer = 0f;
        var duration = 2f;

        while (timer < duration) {
            timer += Time.deltaTime;
            mover.Drop();
            yield return null;
        }
    }

}

public enum UnitychanState {
    IDLE, //停止中
    WALK,
    RUN,
}