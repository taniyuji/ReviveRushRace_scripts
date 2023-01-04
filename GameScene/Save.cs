using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//進めたシーンの次のシーンのインデックス番号を保存するスクリプト
public class Save : MonoBehaviour
{
    //シーンのクリア時に呼ばれる。その次のシーンのインデックス番号を保存する。
    public void SaveSceneNumber()
    {
        //既にクリア済みのステージの場合はセーブしない
        if (SceneManager.GetActiveScene().buildIndex < PlayerPrefs.GetInt("SceneIndex")) return;

        PlayerPrefs.SetInt("SceneIndex", SceneManager.GetActiveScene().buildIndex + 1);

        PlayerPrefs.Save();
    }

    //全てのシーンをクリアした場合に呼ばれる。
    public void ResetSceneNumber()
    {
        PlayerPrefs.SetInt("SceneIndex", 1);

        // Debug.Log(PlayerPrefs.GetInt("SceneIndex"));

        PlayerPrefs.Save();
    }
}
