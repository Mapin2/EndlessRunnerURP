using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name = null;
    public int mixerGroupId = 0;

    public AudioClip clip = null;

    [Range(0f, 1f)]
    public float volume = 0f;
    [Range(.1f, 3f)]
    public float pitch = 0f;

    public bool loop = false;

    [HideInInspector] public AudioSource source = null;
}
