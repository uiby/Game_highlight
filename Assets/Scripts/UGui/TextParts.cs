using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextParts : GuiParts {
    Text text;
    [SerializeField] bool hideOnAwake;
    Coroutine cor;
    bool updating = false;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();
		text = GetComponent<Text>();
        if (hideOnAwake) text.enabled = false;
	}

    public void Hide() {
        text.enabled = false;
    }

    public void Show() {
        text.enabled = true;
    }
    public void Show(string msg) {
        text.enabled = true;
        UpdateText(msg);
    }

    public void UpdateText(string msg) {
        if (updating) StopCoroutine(cor);

        cor = StartCoroutine(ShowTextByCharacter(msg));
    }

    IEnumerator ShowTextByCharacter(string msg) {
        var duration = 0.1f;
        var str = "";
        var length = msg.Length;
        var count = 0;

        updating = true;
        while (count < length) {
            str += msg[count];
            text.text = str;
            yield return new WaitForSeconds(duration);
            count++;
        }
        updating = false;
        yield return null;
    }
}
