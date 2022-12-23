using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using System;

//キャラクターの動きを管理するスクリプト
public class CharacterMover : MonoBehaviour
{
    private float moveSpeed = 2;//線に沿って動く時の速さ

    private float floatSpeed = 0.15f;//待機時の浮遊速度

    private float floatAmount = 0.1f;//待機時の浮遊の上下量

    private ComponentsProvider componentsProvider;//このオブジェクトのコンポーネントを一括管理するスクリプト

    private List<Vector3> path = new List<Vector3>();//lineRendererのPath情報を格納する

    private Sequence sequence;

    private float distance;//lineRendererのpathの長さを格納する

    private Subject<Vector3> _collide = new Subject<Vector3>();

    public IObservable<Vector3> collide//衝突したことをsmokeControllerに通知
    {
        get { return _collide; }
    }

    private Subject<Unit> _goal = new Subject<Unit>();

    public IObservable<Unit> goal//ゴールアニメーションが終了したのちにGoalBehaviorに通知
    {
        get { return _goal; }
    }

    private Subject<Unit> _noGoal = new Subject<Unit>();

    public IObservable<Unit> noGoal//何も衝突せずに失敗したことをCanvasControllerに通知
    {
        get { return _noGoal; }
    }

    private enum CharacterState
    {
        Idle,
        IsMoving,
        IsCollide,
    }

    private CharacterState state = CharacterState.Idle;

    private float floatCounter = 0;

    private float floatAddY = 0;

    void Awake()
    {
        componentsProvider = GetComponent<ComponentsProvider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //全てのキャラのパスが書き終えられた通知を受け取ったらパスに沿って動き出す
        ResourceProvider.i.lineDrawer.finishDrawing.Subscribe(j =>
        {
            for (int i = 0, amount = componentsProvider.lineRenderer.positionCount; i < amount; i++)
            {
                path.Add(componentsProvider.lineRenderer.GetPosition(i));

                if (i == 0) continue;

                distance += Vector3.Distance(componentsProvider.lineRenderer.GetPosition(i), componentsProvider.lineRenderer.GetPosition(i - 1));
            }

            StartCoroutine(Move());

            state = CharacterState.IsMoving;
        });
    }

    private IEnumerator Move()
    {
        yield return null;//パスの代入とdistanceの計算終了を待つ

        sequence = DOTween.Sequence();

        moveSpeed += distance * 0.1f;//距離が長いほど時間がかかるよう調整

        SoundManager.i.PlayOneShot(2, 0.3f);
        //線に沿って動かす
        sequence.Append(transform.DOMove(path[0], 0.1f).SetEase(Ease.OutSine))
                .Append(transform.DOPath(path.ToArray(), moveSpeed - 0.1f, PathType.CatmullRom).SetEase(Ease.OutSine))
                .AppendCallback(() =>
                {
                    if (state != CharacterState.IsCollide)//何も衝突せず失敗した場合
                    {
                        componentsProvider.spriteRenderer.sprite = componentsProvider.sadSprite;
                        //失敗したことを通知
                        _noGoal.OnNext(Unit.Default);
                    }
                }).SetDelay(0.3f);
    }

    void Update()
    {
        if(state == CharacterState.IsMoving) return;
        
        //パスに沿って動き出すまで浮遊アニメーションさせる
        if(floatCounter > floatAmount)
        {
            floatCounter = 0;
            floatAddY *= -1;
        }

        floatCounter += floatSpeed * Time.deltaTime;

        if(floatAddY < 0) floatAddY = -1;
        else  floatAddY = 1;

        floatAddY *= floatSpeed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x,
                                         transform.position.y + floatAddY,
                                         1);


    }

 

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (state == CharacterState.IsCollide) return;

        if (gameObject.tag != collisionInfo.gameObject.tag)//ゴールとの衝突以外の場合
        {
            _collide.OnNext(collisionInfo.contacts[0].point);

            gameObject.SetActive(false);
        }
        else//ゴールに衝突した場合
        {
            sequence = DOTween.Sequence();

            SoundManager.i.PlayOneShot(3, 0.5f);

            sequence.Append(transform.DOScale(Vector3.zero, 0.3f))
                    .Join(transform.DOMove(collisionInfo.transform.position, 0.3f))
                    .AppendCallback(() => _goal.OnNext(Unit.Default));
        }

        state = CharacterState.IsCollide;
    }
}
