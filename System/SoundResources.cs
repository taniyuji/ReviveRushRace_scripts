using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//効果音を管理するスクリプト 
[CreateAssetMenu]
public class SoundResources : ScriptableObject
{
    private static SoundResources _instance;

    //シーン上に実態がないため、スクリプトから参照される場合はprefabから自身をロードして提供する
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
