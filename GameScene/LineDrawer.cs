using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

//各キャラクターのパスを描くスクリプト
public class LineDrawer : MonoBehaviour
{
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();

    private int lineIndex;

    private int judgeAllSet = 0;

    private int lineCount = 0;

    private Subject<Unit> _finishDrawing = new Subject<Unit>();

    public IObservable<Unit> finishDrawing//全てのキャラのパスを書き終えたことを通知する
    {
        get { return _finishDrawing; }
    }

    private RaycastHit2D hit2D;

    private bool isFinishDrawing = false;

    void Start()
    {
        for (int i = 0; i < ResourceProvider.i.charactersAmount; i++)
        {
            lineRenderers.Add(ResourceProvider.i.characters[i].GetComponent<LineRenderer>());
        }
    }

    void Update()
    {
        if (isFinishDrawing) return;

        if (Input.GetMouseButtonDown(0))
        {
            //キャラ以外のLayerはIgnoreRayCastにしてある
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit2D = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            //Debug.Log(hit2D.collider);

            //RayCastより入手したキャラのタグによって扱うlineRendererのインデックスを変更する
            if (hit2D.collider)
            {
                if (hit2D.collider.gameObject.CompareTag("Male"))
                {
                    lineIndex = 0;
                }
                else if (hit2D.collider.gameObject.CompareTag("Women"))
                {
                    lineIndex = 1;
                }
                else if (hit2D.collider.gameObject.CompareTag("Dog"))
                {
                    lineIndex = 2;
                }

                lineRenderers[lineIndex].enabled = true;

                SoundManager.i.PlayOneShot(0, 0.5f);
            }
        }

        if (hit2D.collider == null) return;

        if (Input.GetMouseButton(0))
        {
            var mouseInput = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            
            //カメラ外にパスがかかれないように制限
            if (mouseInput.x > Screen.width)
            {
                mouseInput.x = Screen.width;
            }
            else if (mouseInput.x < 0)
            {
                mouseInput.x = 0;
            }

            if (mouseInput.y > Screen.height)
            {
                mouseInput.y = Screen.height;
            }
            else if (mouseInput.y < 0)
            {
                mouseInput.y = 0;
            }

            mouseInput = Camera.main.ScreenToWorldPoint(mouseInput);

            lineCount++;

            lineRenderers[lineIndex].positionCount = lineCount;

            lineRenderers[lineIndex].SetPosition(lineCount - 1, mouseInput);
        }

        if (Input.GetMouseButtonUp(0))
        {
            for (int i = 0, amount = lineRenderers.Count; i < amount; i++)
            {
                if (lineRenderers[i].enabled)
                {
                    judgeAllSet++;
                }
            }
    
            if (judgeAllSet == lineRenderers.Count)
            {
                _finishDrawing.OnNext(Unit.Default);
                isFinishDrawing = true;
            }

            judgeAllSet = 0;

            lineCount = 0;

            SoundManager.i.PlayOneShot(1, 0.5f);
        }
    }
}
