using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLight : MonoBehaviour {
    [SerializeField] Transform searchLightTrans;
    [SerializeField] Transform frashLightTrans;
    Light searchLight;
    Light frashLight;

    int minBrightness = 0;
    int maxBrightness = 10;
    Coroutine cor;
    bool playingFadeAnimation = false;

    void Awake() {
        searchLight = searchLightTrans.GetComponent<Light>();
        frashLight = frashLightTrans.GetComponent<Light>();
        frashLight.enabled = false;
    }

    public void ChangeDirection() {
        var pos = searchLightTrans.position;
        pos.z *= -1;
        searchLightTrans.position = pos;
        pos = frashLightTrans.position;
        pos.z *= -1;
        frashLightTrans.position = pos;
    }

    public void On() {
        //searchLight.enabled = true;
        if (playingFadeAnimation) StopCoroutine(cor);
        cor = StartCoroutine(FadeIn());
    }

    public void Off() {
        if (playingFadeAnimation) StopCoroutine(cor);
        playingFadeAnimation = false;
        searchLight.range = minBrightness;
        //searchLight.enabled = false;
        //if (playingFadeAnimation) StopCoroutine(cor);
        //cor = StartCoroutine(FadeOut());
    }

    public void OnFrashLight() {
        frashLight.enabled = true;
    }

    //明るくする
    IEnumerator FadeIn() {
        var timer = 0f;
        var rate = 0f;
        var duration = 0.3f;
        var nowBrightness = searchLight.range;

        playingFadeAnimation = true;
        while (rate < 1) {
            timer += Time.deltaTime;
            rate = Mathf.Clamp01(timer/duration);
            searchLight.range = Mathf.Lerp(nowBrightness, maxBrightness, rate);
            yield return null;
        }
        playingFadeAnimation = false;
    }

    //暗くする
    IEnumerator FadeOut() {
        var timer = 0f;
        var rate = 0f;
        var duration = 0.3f;
        var nowBrightness = searchLight.range;

        playingFadeAnimation = true;
        while (rate < 1) {
            timer += Time.deltaTime;
            rate = Mathf.Clamp01(timer/duration);
            searchLight.range = Mathf.Lerp(nowBrightness, minBrightness, rate);
            yield return null;
        }
        playingFadeAnimation = false;
    }
}
