using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CanvasController : MonoBehaviour
{
    private int clearCounter;

    private int allCounter;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0, amount = ResourceProvider.i.charactersAmount; i < amount; i++)
        {
            ResourceProvider.i.characters[i].goal.Subscribe(i =>
            {
                clearCounter++;
                allCounter++;
            });

            ResourceProvider.i.characters[i].noGoal.Subscribe(i =>
            {
                allCounter++;
            });

            ResourceProvider.i.characters[i].collide.Subscribe(i =>
            {
                allCounter++;
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(allCounter < ResourceProvider.i.charactersAmount) return;
        
        if (clearCounter >= ResourceProvider.i.charactersAmount)
        {
            StartCoroutine(ChangeCanvas(1));
        }  
        else
        {
            StartCoroutine(ChangeCanvas(2));
        } 
    }

    private IEnumerator ChangeCanvas(int index)
    {
        yield return new WaitForSeconds(1f);

        ResourceProvider.i.canvases[0].gameObject.SetActive(false);
        ResourceProvider.i.canvases[index].gameObject.SetActive(true);

        allCounter = 0;
        clearCounter = 0;
    }
}
