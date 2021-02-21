using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCharacterAnimation : MonoBehaviour
{
    [SerializeField] Animator animator = null;

    private void Awake()
    {
        animator.SetTrigger("Idle");
    }
}
