using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float jumpForce = 7;
    private float runForce = 10;
    private bool jumped = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") && !jumped)
        {
            jumped = true;
            rb2d.velocity = new Vector2(rb2d.velocity[0], jumpForce);
        }
        else if(Input.GetKey("d"))
        {
            if(rb2d.velocity[0] < runForce)
            {
                rb2d.AddForce(new Vector2(runForce,rb2d.velocity[1]));
            }
        }
        else if (Input.GetKey("a"))
        {
            if (rb2d.velocity[0] > -runForce)
            {
                rb2d.AddForce(new Vector2(-runForce,rb2d.velocity[1]));
            }
        }
        else if(Input.GetKeyUp("w"))
        {
            jumped = false;
        }
    }
}
