using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シーンを跨いでもBGMを続けて鳴らすスクリプト
public class BGM : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    private static BGM i;
    void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(i);

            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
