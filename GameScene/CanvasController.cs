using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CanvasController : MonoBehaviour
{
    private int clearCounter;

    private int failedCounter;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0, amount = ResourceProvider.i.charactersAmount; i < amount; i++)
        {
            ResourceProvider.i.characters[i].goal.Subscribe(i =>
            {
                clearCounter++;
            });

            ResourceProvider.i.characters[i].noGoal.Subscribe(i =>
            {
                failedCounter++;
            });

            ResourceProvider.i.characters[i].collide.Subscribe(i =>
            {
                failedCounter++;
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (clearCounter >= ResourceProvider.i.charactersAmount)
        {
            StartCoroutine(ChangeCanvas(1));
        }  
        else if(failedCounter >= ResourceProvider.i.charactersAmount)
        {
            StartCoroutine(ChangeCanvas(2));
        }
    }

    private IEnumerator ChangeCanvas(int index)
    {
        yield return new WaitForSeconds(1f);

        ResourceProvider.i.canvases[0].gameObject.SetActive(false);
        ResourceProvider.i.canvases[index].gameObject.SetActive(true);

        failedCounter = 0;
        clearCounter = 0;
    }
}
