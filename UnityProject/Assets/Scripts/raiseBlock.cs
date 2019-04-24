using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raiseBlock : MonoBehaviour
{
    private Vector3 start;
    private Vector3 target;
    public float x = 0;
    public float y = 10;
    private float speed = 10;
    private float t = 0;
    private bool raise = false;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.localPosition;
        target = start - new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (raise)
        {
            t += Time.deltaTime / speed;
            transform.position = Vector3.Lerp(start, target, t);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "player" && transform.position != start && !raise)
        {
            raise = true;
            t = 0;
        }
    }
}
