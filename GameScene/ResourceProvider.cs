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

    //キャラクター名とそのcharactersリスト上のインデックス番号を結びつける
    public Dictionary<string, int> getCharacterIndex { get; private set; } = new Dictionary<string, int>();

    [SerializeField]
    private List<Canvas> _canvases;

    public List<Canvas> canvases
    {
        get { return _canvases; }
    }

    [SerializeField]
    private Save _save;

    public Save save
    {
        get { return _save; }
    }

    [SerializeField]
    private FadeController _fadeController;

    public FadeController fadeController
    {
        get { return _fadeController; }
    }

    [SerializeField]
    private SceneLoader _sceneLoader;

    public SceneLoader sceneLoader
    {
        get { return _sceneLoader; }
    }

    void Awake()
    {
        i = this;

        for (int i = 0; i < characters.Count; i++)
        {
            getCharacterIndex.Add(characters[i].gameObject.tag, i);
        }
    }
}
