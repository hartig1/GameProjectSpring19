using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    public GameObject player;
    private bool visable;
    private Rigidbody2D rb2d;
    public GameObject swordObj;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //swordObj.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        //swordObj.active = true;
        //rb2d.transform.eulerAngles = new Vector2(45, 0);
        //rb2d.transform.eulerAngles = new Vector2(45, 0);
    }

    void swing()
    {
        swordObj.active = true;
        rb2d.transform.eulerAngles = new Vector2(-45, 0);
    }
}
