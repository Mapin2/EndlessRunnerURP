using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Animator animator = null;

    [SerializeField] int pickUpValue = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player layer
        if (collision.gameObject.layer == 8)
        {
            ScoreManager.Instance.ScorePickUp(pickUpValue);
            animator.SetTrigger("Picked");
            AudioManager.Instance.Play("PickUp");
        }
    }

    public void CollectPickUp()
    {
        Destroy(gameObject);
    }
}
