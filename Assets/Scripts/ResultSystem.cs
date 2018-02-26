using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSystem : MonoBehaviour {
    [SerializeField] TextParts resultText;
    [SerializeField] TextParts commentText;
    [SerializeField] GameObject retryButton;
    [SerializeField] TextParts adText;

    void Awake() {
        retryButton.SetActive(false);
    }

    public void ShowRetryButton() {
        retryButton.SetActive(true);
    }

    public void ShowPercent(float per) {
        resultText.Show("キョリ "+ per.ToString("0.0000") +"パーセント");
    }
    public void ShowPhrase(string phrase) {
        resultText.Show(phrase);
    }
    public void ShowComment(string phrase) {
        commentText.Show(phrase);
    }

    public void ShowAd() {
        adText.Show("ガメンガイ ノ ツイッタボタン カンソウ イタダケル ト ワタシ ウレシイ↑");
    }
}
