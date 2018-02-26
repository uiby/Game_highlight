using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    GameManager gameManager;

    void Awake() {
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (!coll.gameObject.CompareTag("Player")) return;
        if (GameManager.gameMode == GameMode.SEARCH)
            gameManager.StartFrashMode();
    }

}
