using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHand : MonoBehaviour
{
    [SerializeField]
    private Transform targetGoal;

    [SerializeField]
    private float MoveSpeed;

    private LineRenderer line;

    private int lineCount = 0;

    private Vector3 defaultPosition;

    void Awake()
    {
        line = GetComponent<LineRenderer>();

        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);

            return;
        }

        if (transform.position.y < targetGoal.position.y)
        {
            transform.position = new Vector3(transform.position.x,
                                             transform.position.y + MoveSpeed * Time.deltaTime, 
                                             1);

            lineCount++;

            line.positionCount = lineCount;

            line.SetPosition(lineCount - 1, transform.position);
        }
        else
        {
            transform.position = defaultPosition;

            lineCount = 0;

            line.positionCount = 0;
        }
    }
}
