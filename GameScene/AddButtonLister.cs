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
        GoStageSelect,
        StageSelect,
        Title,
    }

    [SerializeField]
    private ButtonType type;

    [SerializeField]
    private int stageNumber;

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
            case ButtonType.GoStageSelect
             :
                targetButton.onClick.AddListener(ResourceProvider.i.sceneLoader.LoadStageSelect);
                break;
            case ButtonType.StageSelect
             :
                targetButton.onClick.AddListener(() => ResourceProvider.i.sceneLoader.LoadSelectedScene(stageNumber));
                break;
            case ButtonType.Title
             :
                targetButton.onClick.AddListener(ResourceProvider.i.sceneLoader.LoadTitle);
                break;
        }

        targetButton.onClick.AddListener(ResourceProvider.i.fadeController.FadeOut);
    }
}
