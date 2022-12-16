using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

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

        if(collisionInfo.gameObject.tag == gameObject.tag)
        {
            mover = collisionInfo.gameObject.GetComponent<CharacterMover>();

            mover.goal.Subscribe(i =>
            {
                spriteRenderer.sprite = goalSprite;

                SoundManager.i.PlayOneShot(4, 0.5f);
            });
        }
    }
}
