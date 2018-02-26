using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField] CharacterProvider playerProvider;
    [SerializeField] UnitychanProvider unitychanProvider;
    [SerializeField] ResultSystem resultSystem;
    [SerializeField] DropTrap dropTrap;
    public static GameMode gameMode{get; private set;}

	// Use this for initialization
	void Start () {
        gameMode = GameMode.SEARCH;
	}

    public void Retry() {
        SceneManager.LoadScene("Main");
    }

    //マリオパーティのわんわん懐中電灯ゲームの始まり
    public void StartFrashMode() {
        StartCoroutine(FrashMode());
    }

    IEnumerator FrashMode() {
        playerProvider.MoveStop();
        playerProvider.Stopping();
        playerProvider.LightOff();
        unitychanProvider.Show();
        dropTrap.ShowImage();

        gameMode = GameMode.FRASH;
        yield return null;
        yield return StartCoroutine(playerProvider.LookLeft());

        unitychanProvider.StartMove();
    }

    //避けるゲーム
    public void StartEscapeMode() {
        gameMode = GameMode.ESCAPE;
    }

    public void PlayerFrashToUnitychan() {
        StartCoroutine(FinishGameByFrashMode());
    }

    IEnumerator FinishGameByFrashMode() {
        playerProvider.OnFrashLight();
        unitychanProvider.StopMove();
        unitychanProvider.LightOn();
        gameMode = GameMode.RESULT;

        var percent = unitychanProvider.GetPercent();

        if (percent > 90) {
            playerProvider.PlayGameClear();
        }
        yield return new WaitForSeconds(1f);
        resultSystem.ShowPercent(percent);

        yield return new WaitForSeconds(1.5f);

        if (percent < 20) resultSystem.ShowComment("エンド 「ハジメテ　ノ　キョリカン」");
        else if (20 <= percent && percent < 90) resultSystem.ShowComment("エンド 「モット　チカヅキタイ」");
        else if (percent > 90) resultSystem.ShowComment("エンド 「チカスギル　ノモ　ワルクナイネ」\nゲームクリア");

        resultSystem.ShowRetryButton();
    }

    public void GameOverByFrash() {
        StartCoroutine(GameOverByFrashMode());
    }

    IEnumerator GameOverByFrashMode() {
        gameMode = GameMode.RESULT;
        playerProvider.MoveStop();
        playerProvider.PlayGameOver();
        yield return new WaitForSeconds(2f);

        resultSystem.ShowPhrase("キョリ　ミチスウ");
        yield return new WaitForSeconds(1.5f);
        resultSystem.ShowComment("エンド 「ワカラナイワ...」");

        resultSystem.ShowRetryButton();
        yield return new WaitForSeconds(5f);
        unitychanProvider.StopMove();
    }

    public void GameOverByEscape() {
        StartCoroutine(GameOverByEscapeMode());
    }

    IEnumerator GameOverByEscapeMode() {
        gameMode = GameMode.RESULT;
        unitychanProvider.StopMove();
        playerProvider.MoveStop();
        yield return null;
        playerProvider.PlayGameOver();
        unitychanProvider.LightOn();
        yield return new WaitForSeconds(1f);
        unitychanProvider.PlayCatchPlayerAnimation();
        yield return new WaitForSeconds(1f);

        resultSystem.ShowPhrase("アタル");
        yield return new WaitForSeconds(1.5f);
        resultSystem.ShowComment("エンド 「ワタシ　カラハ　ニゲレナイ」");
        resultSystem.ShowRetryButton();
    }

    public void TureEnd() {
        StartCoroutine(PlayTureEnd());
    }

    IEnumerator PlayTureEnd() {
        gameMode = GameMode.RESULT;
        playerProvider.MoveStop();
        unitychanProvider.LightOn();
        yield return null;
        playerProvider.Stopping();
        yield return new WaitForSeconds(1f);
        unitychanProvider.PlayTureEndAnimation();

        yield return new WaitForSeconds(2f);
        playerProvider.PlayTureEnd();
        resultSystem.ShowPhrase("キョリ　ケイソクフノウ");
        yield return new WaitForSeconds(1.5f);
        resultSystem.ShowComment("トゥルーエンド 「ハロー!　マイ　フレンド」\nゲームクリア");
        resultSystem.ShowRetryButton();
        yield return new WaitForSeconds(4.5f);
        resultSystem.ShowAd();
    }

    public void SecondEnd() {
        StartCoroutine(PlaySecondEnd());
    }

    IEnumerator PlaySecondEnd() {
        gameMode = GameMode.RESULT;
        playerProvider.MoveStop();
        unitychanProvider.LightOn();
        yield return null;
        playerProvider.Stopping();
        yield return StartCoroutine(playerProvider.LookRight());
        yield return new WaitForSeconds(1f);

        playerProvider.PlaySecondEnd();
        yield return StartCoroutine(dropTrap.Drop());

        yield return new WaitForSeconds(1f);
        resultSystem.ShowPhrase("キョリ　ケンガイ");
        yield return new WaitForSeconds(1.5f);
        resultSystem.ShowComment("セカンドエンド 「コウスルシカ　ナイノ」 \nゲームクリア");
        resultSystem.ShowRetryButton();

        yield return new WaitForSeconds(4.5f);
        resultSystem.ShowAd();
    }
}

public enum GameMode {
    SEARCH, //探検
    FRASH, //点灯
    ESCAPE, //逃げる
    RESULT,
}