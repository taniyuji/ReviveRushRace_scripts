using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoundResources : ScriptableObject
{
    private static SoundResources _instance;

    public static SoundResources Instance
    {
        get
        {
            if(_instance) return _instance;

            _instance = Resources.Load<SoundResources>("ScriptableObject/SEResource");

            if(_instance == null)
            {
                Debug.LogError("failed to load SoundResources");
            }

            return _instance;
        }
    }

    public List<AudioClip> resources;
}
