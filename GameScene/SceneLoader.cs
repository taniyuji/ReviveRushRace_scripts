using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

//シーンの読み込みを管理するスクリプト
public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private FadeController fadeController;

    int index = 0;

    private bool canLoad = false;
    void Start()
    {
        //フェードアウト終了通知を受け取ったらシーン遷移させる
        fadeController.finishFadeOut.Subscribe(i =>
        {
            canLoad = true;
        });
    }
    public void LoadNextScene()
    {
        index = SceneManager.GetActiveScene().buildIndex + 1;

        if (index == 0) return;

        StartCoroutine(LoadScene(index));
    }

    //現在のシーンを再読み込み
    public void LoadNowScene()
    {
        index = SceneManager.GetActiveScene().buildIndex;

        if (index == 0) return;

        StartCoroutine(LoadScene(index));
    }

    //前回までクリアしたシーンの次のシーンを呼び出す
    public void LoadLastScene()
    {
        index = PlayerPrefs.GetInt("SceneIndex");

        if (index == 0) return;

        StartCoroutine(LoadScene(index));
    }

    //タイトルシーンを読み込み
    //タイトルシーンはScenesInBuildの先頭に置く
    public void LoadTitle()
    {
        StartCoroutine(LoadScene(0));
    }

    //ステージ選択シーンをロードする
    public void LoadStageSelect()
    {
        StartCoroutine(LoadSceneByName("StageSelect"));
    }

    //引数で渡されたシーンをロードする
    public void LoadSelectedScene(int index)
    {
        StartCoroutine(LoadScene(index));
    }

    //フェードが終わるまでシーンのロードを待機する
    private IEnumerator LoadScene(int index)
    {
        yield return new WaitUntil(() => canLoad);

        SceneManager.LoadScene(index);
    }

    private IEnumerator LoadSceneByName(string name)
    {
        yield return new WaitUntil(() => canLoad);

        SceneManager.LoadScene(name);
    }


}
