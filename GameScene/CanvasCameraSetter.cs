using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カメラのRenderCameraを動的にセットするスクリプト
//ゲーム画面用と画面の淵を映す用の2つのカメラが存在するため
public class CanvasCameraSetter : MonoBehaviour
{
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
