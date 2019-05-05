using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basket : MonoBehaviour
{
    public lift l;
    private int hits = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "attack")
        {
            if (hits != 0)
            {
                //transform.position = Vector3.Lerp(transform.localPosition, transform.localPosition - new Vector3(0, 2, 0), 1f);
                hits -= 1;
                if(hits == 0)
                {
                    l.move(5f);
                    hits = 1;
                }
            }
        }
    }
}
