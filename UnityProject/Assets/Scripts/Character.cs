﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float jumpForce = 7;
    private float runForce = 10;
    private bool canJump = true;
    private float startingScale;
    public GameObject sword;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startingScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb2d.velocity[0] > .1)
        {
            transform.localScale = new Vector2(startingScale, transform.localScale.y);
        }
        else if(rb2d.velocity[0] < -.1)
        {
            transform.localScale = new Vector2(-startingScale, transform.localScale.y);
        }
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("space")) && canJump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity[0], jumpForce);
            canJump = false;
        }
        else if (Input.GetKey("d"))
        {
            if (rb2d.velocity[0] < runForce)
            {
                rb2d.AddForce(new Vector2(runForce, 0));
            }
        }
        else if (Input.GetKey("a"))
        {
            if (rb2d.velocity[0] > -runForce)
            {
                rb2d.AddForce(new Vector2(-runForce, 0));
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {

            sword.transform.Rotate = Quaternion.Euler(0, 0, -135);
            sword.transform.Position = new Vector3(1, 0, 0);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            sword.transform.Rotate = Quaternion.Euler(0, 0, 135+180);
            sword.transform.Position = new Vector3(0, 1, 0);
        }
    }
    void OnCollisionEnter2D (Collision2D col)
    {
        if(col.gameObject.tag == "ground")
        {
            canJump = true;
        }
    }
}
