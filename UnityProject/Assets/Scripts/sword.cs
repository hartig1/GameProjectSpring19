using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class sword : MonoBehaviour
{
    public GameObject player;
    [SerializeField] public Character character;
    private bool visable;
    private Rigidbody2D rb2d;
    public GameObject swordObj;
    private Vector3 offset;
    private bool trans = false;
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
        if (character.right)
        {
            transform.position = player.transform.position + offset;
            if(trans)
            {
                transform.position += new Vector3(4, 0, 0);
                trans = false;
            }
        }
        else
        {
            transform.position = player.transform.position + offset - new Vector3(4,0,0);
            trans = true;
        }
        //swordObj.active = true;
        //rb2d.transform.eulerAngles = new Vector2(45, 0);
        //rb2d.transform.eulerAngles = new Vector2(45, 0);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        /*if (col.gameObject.tag == "enemy")
        {
            Assert.IsFalse(true);
            Destroy(col.gameObject);
            col.gameObject.SetActive(false);
        }*/
    }
}
