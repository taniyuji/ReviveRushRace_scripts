using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//ボタンのOnClickのlisterを加えるスクリプト
public class AddButtonLister : MonoBehaviour
{
    private Button targetButton;

    public enum ButtonType
    {
        Reset,
        GoNextStage,
        Continue,
    }

    [SerializeField]
    private ButtonType type;

    void Awake()
    {
        targetButton = GetComponent<Button>();
    }

    void Start()
    {
        //ボタンのタイプによってボタンが押されたときにどのシーンを読み込むかを設定する
        switch (type)
        {
            case ButtonType.Reset
             :
                targetButton.onClick.AddListener(ResourceProvider.i.sceneLoader.LoadNowScene); break;

            case ButtonType.GoNextStage
             :
                targetButton.onClick.AddListener(ResourceProvider.i.sceneLoader.LoadNextScene); break;

            case ButtonType.Continue
             :
                if (PlayerPrefs.GetInt("SceneIndex") == 0)
                {
                    gameObject.SetActive(false); 
                    break;
                }
                else
                {
                    gameObject.SetActive(true);
                    targetButton.onClick.AddListener(ResourceProvider.i.sceneLoader.LoadLastScene); 
                    break;
                }
        }

        targetButton.onClick.AddListener(ResourceProvider.i.fadeController.FadeOut);
    }
}
