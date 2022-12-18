using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

//各キャラクターのパスを描くスクリプト
public class LineDrawer : MonoBehaviour
{

    [SerializeField]
    private List<LineRenderer> lineRenderer;

    private int lineIndex;

    private int judgeAllSet = 0;

    private int lineCount = 0;

    private Subject<bool> _finishDrawing = new Subject<bool>();

    public IObservable<bool> finishDrawing//全てのキャラのパスを書き終えたことを通知する
    {
        get { return _finishDrawing; }
    }

    private RaycastHit2D hit2D;

    private bool isFinishDrawing = false;

    void Update()
    {
        if (isFinishDrawing) return;

        if (Input.GetMouseButtonDown(0))
        {
            //キャラ以外のLayerはIgnoreRayCastにしてある
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit2D = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            Debug.Log(hit2D.collider);

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

                lineRenderer[lineIndex].enabled = true;

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

            lineRenderer[lineIndex].positionCount = lineCount;

            lineRenderer[lineIndex].SetPosition(lineCount - 1, mouseInput);
        }

        if (Input.GetMouseButtonUp(0))
        {
            for (int i = 0, amount = lineRenderer.Count; i < amount; i++)
            {
                if (lineRenderer[i].enabled)
                {
                    judgeAllSet++;
                }
            }
    
            if (judgeAllSet == lineRenderer.Count)
            {
                _finishDrawing.OnNext(true);
                isFinishDrawing = true;
            }

            judgeAllSet = 0;

            lineCount = 0;

            SoundManager.i.PlayOneShot(1, 0.5f);
        }
    }
}
