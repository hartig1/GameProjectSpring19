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
    }

    // Update is called once per frame
    void Update()
    {
        if(player.rb2d.velocity[0] >= .01 && player.rb2d.velocity[1] <= .01)
        {
            walk.Play("Walk Animation");
        }
        if (Input.GetKey("d"))
        {
            walk.Play("Walk Animation");
        }
        else if (Input.GetKey("a"))
        {
            walk.Play("Walk Animation");
        }
    }
}
