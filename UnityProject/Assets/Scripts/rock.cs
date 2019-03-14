using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
{
    private bool dir = false;
    private Rigidbody2D rb;
    private bool active = false;
    public GameObject go;
    private float forceH = 5f;
    private bool rolling = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (active && rolling)
        {
            if (dir)
            {
                rb.velocity = new Vector2(forceH, rb.velocity[1]);
            }
            else
            {
                rb.velocity = new Vector2(-forceH, rb.velocity[1]);
            }
        }
    }
    public void Drop(bool dir)
    {
        this.dir = dir;
        active = true;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            rolling = true;
        }
        if (col.gameObject.tag == "enemy" || col.gameObject.tag == "player")
        {
            Destroy(gameObject);
        }
    }
}
