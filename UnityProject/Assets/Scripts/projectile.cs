using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private bool dir;
    private Rigidbody2D rb;
    private bool active;
    private float forceH = 25f;
    private float forceV = 15f;
    public breakable platform;
    public GameObject go;
    public Character ch;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
        Physics2D.IgnoreCollision(ch.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(platform.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (dir)
            {
                rb.velocity = new Vector2(forceH, forceV);
            }
            else
            {
                rb.velocity = new Vector2(-forceH, forceV);
            }
        }
    }

    public void fire(bool right)
    {
        dir = right;
        active = true;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        if (dir)
        {
            rb.velocity = new Vector2(forceH, forceV);
            //this.transform.right = new Vector3(90,0,0);
        }
        else
        {
            rb.velocity = new Vector2(-forceH, forceV);
            //this.transform.right = new Vector3(-90, 0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //rb.constraints = new RigidbodyConstraints2D();
        Destroy(gameObject);
        if(col.gameObject.tag == "ground")
        {
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "basket")
        {
            Destroy(gameObject);
        }
    }
}
