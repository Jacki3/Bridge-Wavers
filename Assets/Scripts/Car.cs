using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

public class Car : PathFollower
{
    public int scoreToAdd;

    [SerializeField]
    private List<GameObject> modelPrefabs = new List<GameObject>();

    [SerializeField]
    private AudioSource beepSource;

    [SerializeField]
    private AudioClip beepSound;

    public void WaveBack()
    {
        Beep();
    }

    public void Beep()
    {
        beepSource.PlayOneShot (beepSound);
    }
}
