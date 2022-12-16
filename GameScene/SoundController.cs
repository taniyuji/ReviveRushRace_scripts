using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SoundController : MonoBehaviour
{
    private int disappointCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0, amount = ResourceProvider.i.characters.Count; i < amount; i++)
        {
            ResourceProvider.i.characters[i].noGoal.Subscribe(i =>
            {
                disappointCounter++;
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(disappointCounter >= ResourceProvider.i.characters.Count)
        {
            SoundManager.i.PlayOneShot(7, 0.5f);

            disappointCounter = 0;
        }
    }
}
