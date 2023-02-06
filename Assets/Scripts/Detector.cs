using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField]
    private int detectorScore;

    [SerializeField]
    private bool isBridge;

    [SerializeField]
    private RoadDetector roadDetector;

    [SerializeField]
    private ScoreGain scoreGain;

    public void CarWaved()
    {
        if (roadDetector.CurrentCar() != null)
        {
            string score =
                (roadDetector.GetCarScore() + detectorScore).ToString();
            Vector3 pos = roadDetector.CurrentCar().transform.position;
            ScoreGain newScoreGain = Instantiate(scoreGain);
            newScoreGain.SetScoreGain(score, pos, Quaternion.identity);
        }
    }

    public int GetCarScore()
    {
        return roadDetector.GetCarScore() + detectorScore;
    }

    public bool CarCheck()
    {
        return roadDetector.HasCar();
    }

    public bool IsBridge()
    {
        return isBridge;
    }
}
