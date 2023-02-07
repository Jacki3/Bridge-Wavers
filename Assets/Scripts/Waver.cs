using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Waver : MonoBehaviour
{
    public string playerName;

    [SerializeField]
    private float waveRate;

    [SerializeField]
    private Color waveColor;

    [SerializeField]
    private Color hitColor;

    [SerializeField]
    private UnityEngine.UI.Image waverImage;

    [SerializeField]
    private AudioSource waverSoundSource;

    [SerializeField]
    private AudioClip waveSound;

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
        if (
            Input.GetKeyUp(KeyCode.E) &&
            Time.time > nextWave &&
            StateManager.gameState == StateManager.State.Playing
        )
        {
            ColorChange(true);
        }
    }

    public void ColorChange(bool isWave)
    {
        if (wave != null)
        {
            wave (playerName);
            if (waverSoundSource != null && waveSound != null)
                waverSoundSource.PlayOneShot(waveSound);
        }
        waverImage.color = isWave ? waveColor : hitColor;
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
