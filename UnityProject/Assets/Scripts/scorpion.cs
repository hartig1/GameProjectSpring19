using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class scorpion : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float startingScale;
    private float walkSpeed = 15;
    private bool dir = true;
    private int timer = 0;
    private int health = 2;
    public GameObject scorp;
    public int dist;
    private Vector2 start;
    private Vector2 end;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        startingScale = transform.localScale.x;
        rb2d.velocity = new Vector2(walkSpeed, 0);
        start = transform.position;
        end = transform.position + new Vector3(dist, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(end.x < transform.position.x && dir)
        {
            dir = false;
            rb2d.velocity = -rb2d.velocity;
        }
        if(transform.position.x < start.x && !dir)
        {
            dir = true;
            rb2d.velocity = -rb2d.velocity;
        }
        if(dir)
        {
            //rb2d.AddForce(new Vector2(.35f*walkSpeed,0));
            rb2d.velocity = (new Vector2(walkSpeed, rb2d.velocity[1]));
            transform.localScale = new Vector2(startingScale, transform.localScale.y);
        }
        else
        {
            //rb2d.AddForce(new Vector2(-.35f*walkSpeed, 0));
            rb2d.velocity = (new Vector2(-walkSpeed, rb2d.velocity[1]));
            transform.localScale = new Vector2(-startingScale, transform.localScale.y);
        }
        if(timer > 0) timer--;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "attack")
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
                scorp.SetActive(false);
            }
            if (dir)
            {
                rb2d.velocity = new Vector2(-walkSpeed, rb2d.velocity[1]);
                rb2d.position = new Vector2(rb2d.position[0] - 1f, rb2d.position[1]);
            }
            else
            {
                rb2d.velocity = new Vector2(walkSpeed, rb2d.velocity[1]);
                rb2d.position = new Vector2(rb2d.position[0] + 1f, rb2d.position[1]);
            }
            dir = !dir;
            timer = 5;
        }
        if(col.gameObject.tag == "projectile")
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
                scorp.SetActive(false);
            }
            timer = 5;
        }
        if(col.gameObject.tag == "player")
        {
            dir = !dir;
        }
    }
}
