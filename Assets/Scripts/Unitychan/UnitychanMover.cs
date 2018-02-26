using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanMover : MonoBehaviour {
    Rigidbody2D m_rigidbody2D;
    UnitychanProvider provider;
    float walkSpeed = 0.7f;
    float runSpeed = 3.5f;

	// Use this for initialization
	void Awake () {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        provider = GetComponent<UnitychanProvider>();
	}

    void FixedUpdate() {
        var state = provider.NowState();
        switch (state) {
            case UnitychanState.WALK: Move(walkSpeed);
            break;
            case UnitychanState.RUN: Move(runSpeed);
            break;
        }
    }

    void Move(float speed) {
        m_rigidbody2D.MovePosition((Vector2)transform.position + Vector2.right * speed * Time.deltaTime);
    }

    public void Drop() {
        m_rigidbody2D.MovePosition((Vector2)transform.position + Vector2.down* 5f * Time.deltaTime);
    }
}
