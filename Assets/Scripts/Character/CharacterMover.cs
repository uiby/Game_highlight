using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour {
    [SerializeField] GameManager gameManager;
    [SerializeField, Range(5, 20)] float maxSpeed = 10f;
    [SerializeField, Range(400, 600)] int jumpPower = 500;

    [HideInInspector]
    public bool jumping = false;
    CharacterProvider charaProvider;
    float prevSign = 1;

    // Use this for initialization
    void Awake () {
        charaProvider = GetComponent<CharacterProvider>();
    }

    public void Stop() {
        charaProvider.m_rigidbody2D.velocity = new Vector2(0, charaProvider.m_rigidbody2D.velocity.y);
    }
	
    public void Move(float move, bool jump) {
        //if (move == 0) return;
        if (Mathf.Abs(move) > 0) {
            Quaternion rot = transform.rotation;
            var sign = Mathf.Sign(move);
            transform.rotation = Quaternion.Euler(rot.x, sign == 1 ? 0 : 180, rot.z);
            if (sign != prevSign) {
                prevSign = sign;
                charaProvider.ChangeDirection();
            }
        }

        charaProvider.m_rigidbody2D.velocity = new Vector2(move * maxSpeed, charaProvider.m_rigidbody2D.velocity.y);
        /*charaProvider.m_rigidbody2D.AddForce(transform.right * move);
        var vec = charaProvider.m_rigidbody2D.velocity;
        if (Mathf.Abs(vec.x) > maxSpeed) {
            vec.x = vec.x < 0 ? -maxSpeed : maxSpeed;
            charaProvider.m_rigidbody2D.velocity = vec;
        }*/

        //transform.Translate(transform.right * move * maxSpeed);
        //JUMP
    }

    public void BeginJump() {
        if (jumping) return;
        jumping = true;
        charaProvider.m_rigidbody2D.AddForce(Vector2.up * jumpPower);
    }

    void EndJump() {
        jumping = false;
        charaProvider.EndJump();
    }

    bool HasGround() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.4f);

        return hit.collider != null;
    }

    Coroutine shakeCor;
    float maxShakeMargin = 0.3f;
    bool shaking = false;
    Vector3 center;
    int shakeCount = 0;
    public void Shake() {
        if (shaking) StopCoroutine(shakeCor);
        else center = transform.position;
        shakeCount++;
        if (shakeCount == 30) {
            gameManager.StartEscapeMode();
            return;
        }
        shakeCor = StartCoroutine(PlayShakeToHorizontal(0.1f, Mathf.Clamp01(0.2f + shakeCount/30f)));
    }

    IEnumerator PlayShakeToHorizontal(float duration, float shakeRate) {
        var timer = 0f;
        var rate = 0f;
        int upDown = 1;

        var margin = maxShakeMargin * shakeRate;

        shaking = true;
        while(rate < 1) {
            timer += Time.deltaTime;
            rate = Mathf.Clamp01(timer/duration);

            transform.position = center + Vector3.right * Mathf.Lerp(margin, 0, rate) * upDown;
            upDown *= -1;
            yield return null;
        }
        shaking = false;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("Ground")) {
            if (!jumping) return;
            EndJump();
        }
    }
}
