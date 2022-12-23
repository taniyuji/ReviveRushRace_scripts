using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//シーンの読み込みを管理するスクリプト
public class SceneLoder : MonoBehaviour
{
    int index = 0;
    public void LoadNextScene()
    {
        index = SceneManager.GetActiveScene().buildIndex + 1;

        if (index == 0) return;

        SceneManager.LoadScene(index);
    }

    public void LoadNowScene()
    {
        index = SceneManager.GetActiveScene().buildIndex;

        if (index == 0) return;

        SceneManager.LoadScene(index);
    }

    //前回までクリアしたシーンの次のシーンを呼び出す
    public void LoadLastScene()
    {
        //Debug.Log(PlayerPrefs.GetInt("SceneIndex"));
        SceneManager.LoadScene(PlayerPrefs.GetInt("SceneIndex"));
    }
}
