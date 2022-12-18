using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シーン上にひとつしかないオブジェクトの参照を管理するスクリプト
public class ResourceProvider : MonoBehaviour
{
    public static ResourceProvider i { get; private set; }

    [SerializeField]
    private LineDrawer _lineDrawer;

    public LineDrawer lineDrawer
    {
        get { return _lineDrawer; }
    }

    [SerializeField]
    private List<CharacterMover> _characters;

    public List<CharacterMover> characters
    {
        get { return _characters; }
    }

    public int charactersAmount
    {
        get { return _characters.Count; }
    }

    [SerializeField]
    private List<Canvas> _canvases;

    public List<Canvas> canvases
    {
        get { return _canvases; }
    }

    void Awake()
    {
        i = this;
    }
}
