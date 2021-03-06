﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaPlatform : MonoBehaviour
{
    private Vector3 start;
    private Vector3 target;
    private int timeRemaining;
    public float x = 0;
    public float y = 10;
    private bool lower = false;
    private bool raise = false;
    private float speed = 1;
    private float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.localPosition;
        target = start - new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (lower)
        {
            t += Time.deltaTime / speed;
            transform.position = Vector3.Lerp(start, target, t);
            if (t >= 1)
            {
                lower = false;
                raise = true;
                t = 0;
            }
        }
        else if(raise)
        {
            t += Time.deltaTime / speed;
            transform.position = Vector3.Lerp(target, start, t);
            if (t >= 1) raise = false;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "player" && transform.position == start)
        {
            lower = true;
            t = 0;
        }
    }
}
