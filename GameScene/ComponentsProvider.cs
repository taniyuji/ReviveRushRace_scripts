using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//各オブジェクトのコンポーネントを管理するスクリプト(GetComponentを極力少なくするため)
public class ComponentsProvider : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;

    public LineRenderer lineRenderer
    {
        get { return _lineRenderer; }
    }

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public SpriteRenderer spriteRenderer
    {
        get { return _spriteRenderer; }
    }

    [SerializeField]
    private Sprite _sadSprite;

    public Sprite sadSprite
    {
        get { return _sadSprite; }
    }
}
