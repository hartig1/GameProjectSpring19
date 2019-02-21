using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float jumpForce = 12;
    private float runForce = 25;
    private bool canJump = true;
    private float startingScale;
    public GameObject sword;
    private bool swordRotated = false;
    public bool right = true;
    private int health = 1;
    private int invinsibleTimer;
    public GameObject player;
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
            right = true;
        }
        else if(rb2d.velocity[0] < -.1)
        {
            transform.localScale = new Vector2(-startingScale, transform.localScale.y);
            right = false;
        }
        if (Input.GetMouseButtonDown(0) && !swordRotated)
        {
            //sword.transform.Rotate(0, 0, -90);
            if (right)
            {
                sword.transform.eulerAngles = new Vector3(sword.transform.eulerAngles.x, sword.transform.eulerAngles.y, -70);
            }
            else
            {
                sword.transform.eulerAngles = new Vector3(sword.transform.eulerAngles.x, sword.transform.eulerAngles.y, 100);
            }
            //sword.transform.Rotate(Vector3.right * Time.deltaTime);
            swordRotated = true;
        }
        else if (Input.GetMouseButtonDown(1) && swordRotated)
        {
            //sword.transform.Rotate(0, 0, 90);
            if (right)
            {
                sword.transform.eulerAngles = new Vector3(sword.transform.eulerAngles.x, sword.transform.eulerAngles.y, 20);
            }
            else
            {
                sword.transform.eulerAngles = new Vector3(sword.transform.eulerAngles.x, sword.transform.eulerAngles.y, 20);
            }
            //sword.transform.Rotate(Vector3.left * Time.deltaTime);
            swordRotated = false;
        }
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("space")) && canJump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity[0], jumpForce);
            canJump = false;
        }
        if (Input.GetKey("d"))
        {
            if (rb2d.velocity[0] < runForce)
            {
                rb2d.AddForce(new Vector2(runForce, 0));
            }
        }
        if (Input.GetKey("a"))
        {
            if (rb2d.velocity[0] > -runForce)
            {
                rb2d.AddForce(new Vector2(-runForce, 0));
            }
        }
        if (invinsibleTimer > 0) invinsibleTimer--;
    }
    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            canJump = true;
        }
        else if (col.gameObject.tag == "enemy" && invinsibleTimer == 0)
        {
            health -= 1;
            invinsibleTimer = 5;
            if(health == 0)
            {
                Destroy(this);
                player.SetActive(false);
            }
        }
    }
}
