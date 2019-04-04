using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public rock r;
    public bool goodRock = false;
    private int spawnTime = 0;
    public int spawnCooldown = 120;
    public bool right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime == 0)
        {
            rock clone = (Instantiate(r, transform.position, transform.rotation)) as rock;
            if (goodRock)
            {
                clone.tag = "goodRock";
            }
            clone.Drop(right);
            spawnTime = spawnCooldown;
            //clone.rigidbody.AddForce(1000, 0, 0);
        }
        else
        {
            spawnTime -= 1;
        }
    }
}
