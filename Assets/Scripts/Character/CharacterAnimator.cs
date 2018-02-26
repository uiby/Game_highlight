using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour {
    CharacterProvider charaProvider;
    Animator animator;

	void Awake () {
        animator = GetComponent<Animator>();
        charaProvider = GetComponent<CharacterProvider>();
	}
	
    void Update() {
        if (charaProvider.IsJumping())
            Jump();
        //Debug.Log("vec:"+GetComponent<Rigidbody2D>().velocity);
    }

    public void Run() {
        animator.SetInteger("Horizontal", 1);

        if (GameManager.gameMode == GameMode.SEARCH || GameManager.gameMode == GameMode.ESCAPE)
            charaProvider.LightOff();
    }

    public void Idle() {
        animator.SetInteger("Horizontal", 0);

        if ((GameManager.gameMode == GameMode.SEARCH || GameManager.gameMode == GameMode.ESCAPE) && 
            charaProvider.m_rigidbody2D.velocity == Vector2.zero) {
            charaProvider.LightOn();
        }
    }

    public void Jump() {
        var vec = charaProvider.m_rigidbody2D.velocity;
        var vertical = vec.y;
        if (vertical < 0) vertical = -1;
        else if (vertical > 0) vertical = 1;
        animator.SetInteger("Vertical", (int)vertical);
    }

    public void EndJump() {
        animator.SetInteger("Vertical", 0);
    }

    public void GameOver() {
        animator.SetInteger("Horizontal", 0);
        animator.SetBool("GameOver", true);
    }
    public void GameClear() {
        animator.SetBool("GameClear", true);
    }
    public void TureEnd() {
        animator.SetInteger("Horizontal", 0);
        animator.SetBool("TureEnd", true);
    }
    public void SecondEnd() {
        animator.SetInteger("Horizontal", 0);
        animator.SetBool("SecondEnd", true);
    }
}
