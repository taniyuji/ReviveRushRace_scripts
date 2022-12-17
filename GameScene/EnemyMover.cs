using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField]
    private bool moveHorizontal;

    [SerializeField]
    private bool firstMinus;

    [SerializeField]
    private float moveSpeed;

    private Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        moveVector = moveHorizontal ? new Vector2(1, 0) : new Vector2(0, 1);
        if(firstMinus) moveVector *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        var fixedMoveSpeed = moveSpeed * Time.deltaTime;

        transform.position += fixedMoveSpeed * moveVector;
    }

    void OnBecameInvisible()
    {
        moveVector *= -1;
    }
}
