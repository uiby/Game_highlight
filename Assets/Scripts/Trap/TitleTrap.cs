using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTrap : Trap {
    [SerializeField] TextParts titleTarts;
    [SerializeField] Light light;

    protected override void Awake() {
        base.Awake();
        light.enabled = false;
    }

    protected override void Action(Collider2D coll) {
        if (!coll.gameObject.CompareTag("Player")) return;
        if (hit) return;
        hit = true;

        titleTarts.Show("ハロー! My Friend.");
        ChangeColor(Color.white);
        light.enabled = true;
    }
}
