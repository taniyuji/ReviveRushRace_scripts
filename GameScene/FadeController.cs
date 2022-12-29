using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

//フェード画面を管理するスクリプト
public class FadeController : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed = 1f;

    [SerializeField]
    private List<Transform> blacks;//left = 0, right = 1, up = 2, down = 3の順で格納

    [SerializeField]
    private List<Sprite> fadeCharacters;

    [SerializeField]
    private SpriteRenderer fadeCharacter;

    [SerializeField]
    private bool isFadeIn = true;

    private bool isFinishFade = false;

    private Vector3 screenCenterPos;

    private Vector3 screenEdgePos;

    private Subject<Unit> _finishFadeOut = new Subject<Unit>();

    public IObservable<Unit> finishFadeOut
    {
        get
        {
            return _finishFadeOut;
        }
    }

    void Awake()
    {
        //各spriteをカメラの真ん中のポジションに配置する
        screenCenterPos = new Vector3(Screen.width / 2, Screen.height / 2, 1);

        screenCenterPos = Camera.main.ScreenToWorldPoint(screenCenterPos);

        screenEdgePos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));

        fadeCharacter.sprite = fadeCharacters[UnityEngine.Random.Range(0, fadeCharacters.Count)];

        fadeCharacter.transform.position = screenCenterPos;

        fadeCharacter.transform.localScale = Vector3.zero;

        for (int i = 0; i < blacks.Count; i++)
        {
            blacks[i].transform.position = screenCenterPos;
        }
    }

    void Update()
    {
        if (isFinishFade) return;

        Fade();
    }

    //ボタンが押された場合に呼ばれる。フェードアウトを開始する
    public void FadeOut()
    {
        isFadeIn = false;

        isFinishFade = false;

        for (int i = 0; i < blacks.Count; i++)
        {
            blacks[i].gameObject.SetActive(true);
        }

        fadeCharacter.gameObject.SetActive(true);
    }

    private void Fade()
    {
        //右側の黒画像がスクリーンの右端のポジションより外に出た場合にフェードインを終了
        if (isFadeIn && fadeCharacter.transform.localScale.x > 3)
        {
            isFinishFade = true;

            for (int i = 0; i < blacks.Count; i++)
            {
                blacks[i].gameObject.SetActive(false);
            }

            fadeCharacter.gameObject.SetActive(false);

            return;
        }
        //右側の黒画像がスクリーンの真ん中のポジションより左側に入った場合にフェードアウトを終了
        else if (!isFadeIn && fadeCharacter.transform.localScale.x < 0.02f)
        {
            isFinishFade = true;

            _finishFadeOut.OnNext(Unit.Default);

            return;
        }

        //各方向に動く際の速さを求める
        var fixedSpeed = fadeSpeed * Time.deltaTime;

        var moveLeft = Vector3.left * fixedSpeed;

        var moveRight = Vector3.right * fixedSpeed;
        //縦長の画面のため、上下方向の移動は左右方向の移動よりも速くさせる
        var moveUp = Vector3.up * fixedSpeed * 1.7f;

        var moveDown = Vector3.down * fixedSpeed * 1.7f;

        blacks[0].transform.position = isFadeIn ?
                                        blacks[0].transform.position + moveLeft :
                                        blacks[0].transform.position + moveRight;

        blacks[1].transform.position = isFadeIn ?
                                 blacks[1].transform.position + moveRight :
                                 blacks[1].transform.position + moveLeft;

        blacks[2].transform.position = isFadeIn ?
                                blacks[2].transform.position + moveUp :
                                blacks[2].transform.position + moveDown;

        blacks[3].transform.position = isFadeIn ?
                                blacks[3].transform.position + moveDown :
                                blacks[3].transform.position + moveUp;

        var addAmount
             = isFadeIn ? Vector3.one * fixedSpeed : -Vector3.one * fixedSpeed;

        fadeCharacter.transform.localScale += addAmount * 0.5f;
    }
}
