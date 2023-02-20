using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEditor.Experimental;
using UnityEngine;

public class Car : PathFollower
{
    public int scoreToAdd;

    [SerializeField]
    private AudioSource beepSource;

    [SerializeField]
    private AudioClip beepSound;

    [SerializeField]
    private Vector3 carScale;

    [SerializeField]
    private float timeToScale = 1.0f;

    private void OnEnable()
    {
        StartCoroutine(LerpFunction(carScale, timeToScale));
    }

    public void WaveBack()
    {
        Beep();
    }

    public void Beep()
    {
        beepSource.PlayOneShot (beepSound);
    }

    IEnumerator LerpFunction(Vector3 endValue, float duration)
    {
        float time = 0;
        Vector3 startValue = transform.localScale;
        while (time < duration)
        {
            transform.localScale =
                Vector3.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endValue;
    }
}
