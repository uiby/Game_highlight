using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Collider2D))]
public class Trap : MonoBehaviour {
    [SerializeField] bool hideOnAwake;
    SpriteRenderer sprite;
    Collider2D collider;
    protected bool hit;

    protected virtual void Awake () {
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        hit = false;
        if (hideOnAwake) HideImage();
    }

    public void ChangeColor(Color color) {
        sprite.color = color;
    }

    public void ShowImage() {
        sprite.enabled = true;
        collider.enabled = true;
    }

    public void HideImage() {
        sprite.enabled = false;
        collider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        Action(coll);
    }

    protected virtual void Action(Collider2D coll){}

}
