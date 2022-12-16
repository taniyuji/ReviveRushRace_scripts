using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using System;

public class CharacterMover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private ComponentsProvider componentsProvider;

    private List<Vector3> path = new List<Vector3>();

    private Sequence sequence;

    private bool canMove = false;

    private float distance;

    private Subject<Vector3> _collide = new Subject<Vector3>();

    public IObservable<Vector3> collide
    {
        get { return _collide; }
    }

    private Subject<bool> _goal = new Subject<bool>();

    public IObservable<bool> goal
    {
        get { return _goal; }
    }

    private Subject<bool> _noGoal = new Subject<bool>();

    public IObservable<bool> noGoal
    {
        get { return _noGoal; }
    }

    private bool isCollide = false;

    void Awake()
    {
        componentsProvider = GetComponent<ComponentsProvider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResourceProvider.i.lineDrawer.finishDrawing.Subscribe(j =>
        {
            for (int i = 0, amount = componentsProvider.lineRenderer.positionCount; i < amount; i++)
            {
                path.Add(componentsProvider.lineRenderer.GetPosition(i));

                if (i == 0) continue;

                distance += Vector3.Distance(componentsProvider.lineRenderer.GetPosition(i), componentsProvider.lineRenderer.GetPosition(i - 1));
            }

            StartCoroutine(Move());

            canMove = true;
        });
    }

    private IEnumerator Move()
    {
        yield return null;

        sequence = DOTween.Sequence();

        moveSpeed += distance * 0.1f;

        SoundManager.i.PlayOneShot(2, 0.3f);

        sequence.Append(transform.DOMove(path[0], 0.1f).SetEase(Ease.OutSine))
                .Append(transform.DOPath(path.ToArray(), moveSpeed - 0.1f, PathType.CatmullRom).SetEase(Ease.OutSine))
                .AppendCallback(() => 
                {
                    if (!isCollide)
                    {
                        componentsProvider.spriteRenderer.sprite = componentsProvider.sadSprite;
                        _noGoal.OnNext(true);
                    }
                }).SetDelay(0.3f);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (isCollide) return;

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
                    .AppendCallback(() => _goal.OnNext(true));
        }

        isCollide = true;
    }
}
