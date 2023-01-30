using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MovingObject
{
    public int scoreToAdd;

    [SerializeField]
    private AudioSource beepSource;

    [SerializeField]
    private AudioClip beepSound;

    public override void WaveBack()
    {
        beepSource.PlayOneShot (beepSound);
    }
}
