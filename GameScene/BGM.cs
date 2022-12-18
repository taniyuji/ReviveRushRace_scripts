using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            audioSource.time = 12f;

            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
