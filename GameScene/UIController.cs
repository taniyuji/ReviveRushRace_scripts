using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI stageNum;

    // Start is called before the first frame update
    void Start()
    {
        stageNum.text = "Stage" + (SceneManager.GetActiveScene().buildIndex).ToString();
    }
}
