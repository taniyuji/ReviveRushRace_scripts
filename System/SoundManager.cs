using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager i { get; private set; }

    [SerializeField] private AudioSource audioSource;

    private bool isStop = false;

    void Awake()
    {
        i = this;
    }

    [RuntimeInitializeOnLoadMethod()]
    static void Init()
    {
        var prefab = Resources.Load("System/SoundManager");
        var SoundManager = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(SoundManager);
        Debug.Log("Created SM");
    }
    
    public void PlayOneShot(int index, float volume)
    {
        if(SoundResources.Instance.resources.Count - 1 < index) return;

        if(isStop) audioSource.Stop();

        AudioClip clip = SoundResources.Instance.resources[index];

        if(clip == null) return;

        audioSource.PlayOneShot(clip, volume);

        if(index == 2) isStop = true;
        else isStop = false;
    }
}
