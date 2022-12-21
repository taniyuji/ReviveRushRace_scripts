using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

//クリア、ゲームオーバー画面を管理するスクリプト
public class CanvasController : MonoBehaviour
{
    private int clearCounter;

    private int allCounter;

    // Start is called before the first frame update
    void Start()
    {
        //各キャラクターから送られてくる進行結果を取得
        for (int i = 0, amount = ResourceProvider.i.charactersAmount; i < amount; i++)
        {
            ResourceProvider.i.characters[i].goal.Subscribe(i =>
            {
                clearCounter++;
                allCounter++;
            });

            ResourceProvider.i.characters[i].noGoal.Subscribe(i =>
            {
                allCounter++;
            });

            ResourceProvider.i.characters[i].collide.Subscribe(i =>
            {
                allCounter++;
            });
        }

        ResourceProvider.i.canvases[0].gameObject.SetActive(true);
        ResourceProvider.i.canvases[1].gameObject.SetActive(false);
        ResourceProvider.i.canvases[2].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (allCounter < ResourceProvider.i.charactersAmount) return;

        //全てのキャラがクリア状態になった時のみクリアキャンバスを表示
        if (clearCounter >= ResourceProvider.i.charactersAmount)
        {
            StartCoroutine(ChangeCanvas(1));
        }
        else
        {
            StartCoroutine(ChangeCanvas(2));
        }

        allCounter = 0;
        clearCounter = 0;
    }

    private IEnumerator ChangeCanvas(int index)
    {
        yield return new WaitForSeconds(1f);

        ResourceProvider.i.canvases[0].gameObject.SetActive(false);
        ResourceProvider.i.canvases[index].gameObject.SetActive(true);

        if (index == 1)//クリアした場合
        {
            SoundManager.i.PlayOneShot(5, 0.5f);

            ResourceProvider.i.save.SaveSceneNumber();
        }
        else
        {
            SoundManager.i.PlayOneShot(7, 0.3f);


        }
    }
}
