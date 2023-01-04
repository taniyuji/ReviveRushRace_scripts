using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//画面の大きさを可変にするスクリプト
//どんな画面サイズの機種にも対応させるため
[ExecuteAlways]
public class ScreenAspectKeeper : MonoBehaviour
{
    [SerializeField]//基準の画面サイズ
    private Vector2 aspectVector = new Vector2(540, 960);

    void Update()
    {
        //ゲームを起動している機種のアスペクト比を取得
        var screenAspect = Screen.width / (float)Screen.height;
        //基準の画面サイズのアスペクト比を取得
        var targetAspect = aspectVector.x / aspectVector.y;
        //基準のアスペクト比にするために乗算する倍率を計算
        var magRate = targetAspect / screenAspect;

        //カメラのviewPortRectに計算結果を代入する
        var viewPortRect = new Rect(0, 0, 1, 1);

        //現在の画面の横幅か縦幅が小さすぎる場合
        if (magRate < 1)
        {
            viewPortRect.width = magRate;
            viewPortRect.x = 0.5f - viewPortRect.width * 0.5f;
        }
        else
        {
            viewPortRect.height = 1 / magRate;
            viewPortRect.y = 0.5f - viewPortRect.height * 0.5f;
        }

        Camera.main.rect = viewPortRect;
    }
}
