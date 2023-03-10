using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

//ゴールの挙動を扱うスクリプト
public class GoalBehavior : MonoBehaviour
{
    [SerializeField]
    private Sprite goalSprite;

    private SpriteRenderer spriteRenderer;

    private CharacterMover mover;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(mover != null) return;

        //各ゴールとキャラクターは同じタグにしてある。例えば、男性のキャラクターとそのゴールのタグは同じ
        if(collisionInfo.gameObject.tag == gameObject.tag)
        {
            mover = collisionInfo.gameObject.GetComponent<CharacterMover>();

            //Characterのゴールアニメーション終了通知を受け取った場合生き返るのスプライトに切り替え
            mover.goal.Subscribe(i =>
            {
                spriteRenderer.sprite = goalSprite;

                spriteRenderer.color = new Color(1, 1, 1, 1);

                SoundManager.i.PlayOneShot(4, 0.5f);
            });
        }
    }
}
