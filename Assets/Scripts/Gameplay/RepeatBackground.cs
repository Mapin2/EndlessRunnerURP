using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    Vector2 startPosition = Vector2.zero;
    float repeatWidth = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPosition.x - repeatWidth)
        {
            transform.position = startPosition;
        }
    }
}
