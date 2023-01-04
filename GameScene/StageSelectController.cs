using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ステージボタンの表示数を管理するスクリプト
public class StageSelectController : MonoBehaviour
{

    [SerializeField]
    private List<AddButtonLister> stageButtons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = PlayerPrefs.GetInt("SceneIndex"); i < stageButtons.Count; i++)
        {
            if(i == 0) continue;
            
            stageButtons[i].gameObject.SetActive(false);
        }
    }
}
