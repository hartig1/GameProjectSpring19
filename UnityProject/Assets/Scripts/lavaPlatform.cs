using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaPlatform : MonoBehaviour
{
    private Vector3 start;
    private Vector3 target;
    private float transitionTime = 20f;
    private int timeRemaining;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.localPosition;
        target = start - new Vector3(0, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "player")
        {
            timeRemaining = 2;
            transform.position = Vector3.Lerp(transform.localPosition, target, transitionTime);
            Invoke("_tick", 1f);
        }
    }
    private void _tick()
    {
        timeRemaining--;
        if (timeRemaining > 0)
        {
            Invoke("_tick", 1f);
        }
        else
        {
            transform.position = Vector3.Lerp(target, start, transitionTime);
        }
    }
}
