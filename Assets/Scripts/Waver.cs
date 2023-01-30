using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Waver : MonoBehaviour
{
    public string playerName;

    [SerializeField]
    private float waveRate;

    [SerializeField]
    private Color waveColor;

    [SerializeField]
    private UnityEngine.UI.Image waverImage;

    private Color defaultColor;

    private float nextWave;

    public delegate void WaveHandler(string name);

    public static event WaveHandler wave;

    void Start()
    {
        defaultColor = waverImage.color;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && Time.time > nextWave)
        {
            Wave (playerName);
        }
    }

    private void Wave(string name)
    {
        if (wave != null) wave(playerName);
        waverImage.color = waveColor;
        nextWave = Time.time + waveRate;
        StartCoroutine(LerpColourWave());
    }

    IEnumerator LerpColourWave()
    {
        float time = 0;
        Color startValue = waverImage.color;
        while (time < waveRate)
        {
            waverImage.color =
                Color.Lerp(startValue, defaultColor, time / waveRate);
            time += Time.deltaTime;
            yield return null;
        }
        waverImage.color = defaultColor;
    }
}