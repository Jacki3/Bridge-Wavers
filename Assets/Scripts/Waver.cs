using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using MoreMountains.Feedbacks;
public class Waver : MonoBehaviour
{
    public string playerName;

    [SerializeField]
    private float waveRate;

    [Header("Feedbacks")]
    [SerializeField]
    private MMFeedbacks waveFeedback;
    [SerializeField]
    private MMFeedbacks hitFeedback;

    private float nextWave;

    public delegate void WaveHandler(string name);

    public static event WaveHandler wave;

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
        if (wave != null && isWave)
        {
            waveFeedback?.PlayFeedbacks();
            wave (playerName);
            nextWave = Time.time + waveRate;
        }
        else
        {
            hitFeedback?.PlayFeedbacks();
        }
    }
}
