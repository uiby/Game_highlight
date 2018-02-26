using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanController : MonoBehaviour {
    [SerializeField] Transform playerTrans;
    [SerializeField] CameraProvider cameraProvider;
    [SerializeField] GameManager gameManager;
    UnitychanProvider provider;
    UnitychanState state;

    float maxDistance = 0;
    bool isCry = false;

	// Use this for initialization
	void Awake () {
        provider = GetComponent<UnitychanProvider>();
        state = UnitychanState.IDLE;
	}
	
	// Update is called once per frame
	void Update () {
        if (state == UnitychanState.IDLE) return;

        var distance = GetDistance();

        if (state == UnitychanState.WALK && distance < 7.5f) {
            provider.StartRun();
            state = UnitychanState.RUN;
        }

        if (GameManager.gameMode == GameMode.ESCAPE) {
            var isLeftAtPlayerPos = (playerTrans.position.x + 1) < transform.position.x ? true : false;

            if (!isLeftAtPlayerPos) return;

            StopMove();
            provider.PlayEscapedAnimation();
            isCry = true;
        }
		//Debug.Log("Distance:"+GetDistance());
	}

    public bool IsLeftAtPlayerPos() {
        return playerTrans.position.x < transform.position.x;
    }

    public void StartMove() {
        if (state != UnitychanState.IDLE) return;
        maxDistance = GetDistance();
        state = UnitychanState.WALK;
        StartCoroutine(FootStep(GetDistance()));
    }

    public void StopMove() {
        state = UnitychanState.IDLE;
    }

    public UnitychanState NowState() {
        return state;
    }

    public float GetDistance() {
        return Mathf.Abs((playerTrans.position.x-0.1f) - (transform.position.x + 0.06f));
    }

    public float GetPercent() {
        return ((maxDistance - GetDistance()) / maxDistance) * 100f;
    }

    IEnumerator FootStep(float maxDistance) {
        var timer = 0f;
        var rate = 0f;
        while (state == UnitychanState.WALK) {
            timer += Time.deltaTime;
            if (timer > 0.5f) {
                rate = Mathf.Clamp01(1.1f - GetDistance()/maxDistance);  
                provider.PlayFootStep(Random.Range(0, 3), rate);
                cameraProvider.Shake(0.2f, rate);
                timer = 0;
            }
            yield return null;
        }

        timer = 0;
        while (state == UnitychanState.RUN) {
            timer += Time.deltaTime;
            if (timer > 0.15f) {
                rate = Mathf.Clamp01(1.1f - GetDistance()/maxDistance);  
                provider.PlayFootStep(Random.Range(0, 3), rate);
                cameraProvider.Shake(0.2f, rate);
                timer = 0;
            }
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.CompareTag("Player")) {
            switch (GameManager.gameMode) {
                case GameMode.FRASH: gameManager.GameOverByFrash(); break;
                case GameMode.ESCAPE:
                    if (!isCry) gameManager.GameOverByEscape();
                    else gameManager.TureEnd();
                    break;
            }
            
        }
    }

}
