using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] bool playConfirm = false;

    public void AnimButton()
    {
        if (playConfirm)
        {
            AudioManager.Instance.Play("ConfirmButton");
        }
        else
        {
            AudioManager.Instance.Play("CancelButton");
        }
    }
}
