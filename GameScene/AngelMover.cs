using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//天使（敵）を動かすスクリプト
public class AngelMover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;

    private CharacterMover target;

    void OnTriggerEnter2D(Collider2D other)
    {
        //既に追尾対象のキャラクター情報を入手している場合は返す
        if (target != null) return;

        target = other.GetComponent<CharacterMover>();
    }

    void Update()
    {
        //追尾対象のキャラクターの情報を得た場合に追尾開始
        if (target == null) return;

        var step = moveSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);

        //Debug.Log(target.gameObject.transform.position);

        if (!target.gameObject.activeSelf)
        {
            target = null;
        }
    }
}
