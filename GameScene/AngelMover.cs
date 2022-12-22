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
        //既に目標のキャラクター情報を入手している場合は返す
        if (target != null) return;

        //ゴールとキャラクターのタグが同じのため、CharacterMoverを取得して衝突したのがキャラクターかを確認
        if (other.gameObject.CompareTag("Male") || other.gameObject.CompareTag("Women") || other.gameObject.CompareTag("Dog"))
        {
            target = other.GetComponent<CharacterMover>();

            //Debug.Log(target);
        }
    }

    void Update()
    {
        //目標のキャラクターの情報を得た場合にその目標に向かって動かす
        if (target == null) return;

        var step = moveSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);

        Debug.Log(target.gameObject.transform.position);

        if (!target.gameObject.activeSelf)
        {
            target = null;
        }
    }
}
