using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrap : Trap {
    [SerializeField] GameManager gameManager;
    [SerializeField] UnitychanProvider unitychanProvider;
    [SerializeField] LayerMask groundMask;
    protected override void Action(Collider2D coll) {
        if (!coll.gameObject.CompareTag("Player")) return;
        if (hit) return;
        hit = true;
        ChangeColor(Color.white);
        gameManager.SecondEnd();
    }

    public IEnumerator Drop() {
        var pos = unitychanProvider.GetPos();
        var ground = GetGround(pos);
        if (ground == null) yield break;
        yield return null;

        yield return new WaitForSeconds(1f);
        ground.gameObject.SetActive(false);
        if (ground.position.x < pos.x) {//キャラクタより左にある場合
            pos.x += 1;
        } else {
            pos.x -= 1;
        }
        ground = GetGround(pos);
        if (ground != null) {
            ground.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.4f);
        yield return StartCoroutine(unitychanProvider.Drop());
    }

    public Transform GetGround(Vector2 pos) {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(pos, Vector2.down, 1f, groundMask);

        return hit.transform;
    }
}
