using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
