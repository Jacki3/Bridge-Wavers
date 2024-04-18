using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField]
    private List<WaveDetector> waveDetectors = new List<WaveDetector>();

    private void OnEnable()
    {
        Waver.wave += CheckWave;
    }

    private void OnDisable()
    {
        Waver.wave -= CheckWave;
    }

    private void CheckWave(string name)
    {
        foreach (WaveDetector detector in waveDetectors)
        if (detector.HasPlayer())
        {
            if (detector.CheckWaveBack(name))
            {
                int score = detector.GetScore();
                Car car = detector.CurrentCar();

                ScoreController.AddScoreStatic (score);

                car.WaveBack();
            }
        }
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
