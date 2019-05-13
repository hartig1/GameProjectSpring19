using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkController : MonoBehaviour
{
    public Animator walk;
    public Character player;
    // Start is called before the first frame update
    void Start()
    {
        walk = GetComponent<Animator>();
        //walk.SetBool("Idle", true);
        walk.ResetTrigger("Throw");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(walk.GetBool("Idle"));
        if (walk.GetBool("Idle"))
        {
            if (Input.GetKey("d"))
            {
                //Debug.Log("Jump");
                //walk.Play("Throw Animation", 0, 0);
                //walk.SetBool("Idle", false);
                walk.ResetTrigger("Throw");
                walk.SetTrigger("Throw");
            }
            else if (Input.GetKey("a"))
            {
                //Debug.Log("Jump");
                //walk.Play("Throw Animation", 0, 0);
                //walk.SetBool("Idle", false);
            }
        }
    }
}
