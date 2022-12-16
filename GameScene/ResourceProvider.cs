using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProvider : MonoBehaviour
{
    public static ResourceProvider i { get; private set; }

    [SerializeField]
    private GameManager _gameManager;

    public GameManager gameManager
    {
        get { return _gameManager; }
    }

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
