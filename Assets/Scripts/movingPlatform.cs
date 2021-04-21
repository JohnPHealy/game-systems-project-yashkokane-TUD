using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public GameObject platform;

    public float moveSpeed;

    public Transform currentPosition;

    public Transform[] points;

    public int pointSelect;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = points[pointSelect];
    }

    // Update is called once per frame
    void Update()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPosition.position,
            moveSpeed * Time.deltaTime);

        if (platform.transform.position == currentPosition.position)
        {
            pointSelect++;

            if (pointSelect == points.Length)
            {
                pointSelect = 0;
            }

            currentPosition = points[pointSelect];
        }
    }
}
