using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイトル画面の続きからゲームをプレイするボタンの表示を管理するスクリプト
public class ContinueButton : MonoBehaviour
{
    void Start()
    {
        //まだゲーム進度をセーブしていない場合はボタンを表示させない
        if (PlayerPrefs.GetInt("SceneIndex") == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
