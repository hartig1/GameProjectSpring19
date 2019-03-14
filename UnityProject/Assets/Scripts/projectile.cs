using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private bool dir;
    private Rigidbody2D rb;
    private bool active;
    private float force = 20f;
    public GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (dir)
            {
                rb.velocity = new Vector2(force, 0);
            }
            else
            {
                rb.velocity = new Vector2(-force, 0);
            }
        }
    }

    public void fire(bool right)
    {
        dir = right;
        active = true;
        rb = GetComponent<Rigidbody2D>();
        if (dir)
        {
            rb.velocity = new Vector2(force, 0);
            this.transform.right = new Vector3(90,0,0);
        }
        else
        {
            rb.velocity = new Vector2(-force, 0);
            this.transform.right = new Vector3(-90, 0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "ground")
        {
            Destroy(gameObject);
        }
        if(col.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
        }
    }
}
