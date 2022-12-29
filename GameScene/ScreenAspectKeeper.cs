using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ScreenAspectKeeper : MonoBehaviour
{
    [SerializeField]
    private Vector2 aspectVector = new Vector2(540, 960);

    void Update()
    {
        var screenAspect = Screen.width / (float)Screen.height;

        var targetAspect = aspectVector.x / aspectVector.y;

        var magRate = targetAspect / screenAspect;

        var viewPortRect = new Rect(0, 0, 1, 1);

        if (magRate < 1)
        {
            viewPortRect.width = magRate;
            viewPortRect.x = 0.5f - viewPortRect.width * 0.5f;
        }
        else
        {
            viewPortRect.height = 1 / magRate;
            viewPortRect.y = 0.5f - viewPortRect.height * 0.5f;
        }

        Camera.main.rect = viewPortRect;
    }
}
