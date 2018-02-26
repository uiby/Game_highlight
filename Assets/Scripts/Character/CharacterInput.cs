using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour {
    [SerializeField] GameManager gameManager;
    CharacterProvider charaProvider;

	void Awake () {
		charaProvider = GetComponent<CharacterProvider>();
	}
	
	void Update () {
        var mode = GameManager.gameMode;
        switch(mode) {
            case GameMode.SEARCH: Search(); break;
            case GameMode.FRASH: Frash(); break;
            case GameMode.ESCAPE: Search(); break;
        }
	}

    void Search() {
        float x = Input.GetAxis("Horizontal");

        charaProvider.Move(x ,false);
        if (x == 0) charaProvider.Stopping();
        else charaProvider.Running();

        var jump = Input.GetKeyDown(KeyCode.Space);
        if (jump) charaProvider.BeginJump();
    }

    void Frash() {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
            charaProvider.Shake();
        }
        var frash = Input.GetKeyDown(KeyCode.Space);
        if (frash) {
            gameManager.PlayerFrashToUnitychan();
        }
    }
}
