using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftItem : MonoBehaviour
{
    [SerializeField] float speed = 0f, leftBound = -3f;

    MoveLeft moveLeftGround = null;

    void Start()
    {
        // Get move left script from the ground
        moveLeftGround = GameObject.FindGameObjectWithTag("Ground").GetComponent<MoveLeft>();
    }

    void Update()
    {
        // Obstacle speed = to the ground speed
        speed = moveLeftGround.speed;

        if (GameManager.Instance.isGameActive)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
