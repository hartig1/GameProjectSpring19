using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    public GameObject player;
    private bool visable;
    private Rigidbody2D rb2d;
    public GameObject swordObj;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        offset = transform.position - player.transform.position;
        //swordObj.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        //swordObj.active = true;
        //rb2d.transform.eulerAngles = new Vector2(45, 0);
        //rb2d.transform.eulerAngles = new Vector2(45, 0);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            Destroy(col.gameObject);
        }
    }
}
