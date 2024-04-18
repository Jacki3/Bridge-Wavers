using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDetector : MonoBehaviour
{
    [SerializeField]
    private int detectorScore;

    [SerializeField]
    private List<CarDetector> relativeDetectors = new List<CarDetector>();

    [SerializeField]
    private List<string> wavers = new List<string>();

    private CarDetector currentDetector;

    private bool hasPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hasPlayer = true;
            string name = other.GetComponentInChildren<Waver>().playerName;
            wavers.Add (name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            hasPlayer = false;
            string name = other.GetComponentInChildren<Waver>().playerName;
            wavers.Remove (name);
        }
    }

    public bool HasPlayer() => hasPlayer;

    public bool CheckWaveBack(string playerName)
    {
        foreach (string player in wavers)
        if (player == playerName)
        {
            foreach (CarDetector detector in relativeDetectors)
            {
                if (detector.HasCar())
                {
                    currentDetector = detector;
                    return true;
                }
            }
        }
        currentDetector = null;
        return false;
    }

    public int GetScore() => detectorScore + currentDetector.GetScore();

    public Car CurrentCar() => currentDetector.GetCar();

    void Start()
    {
    }

    void Update()
    {
    }
}
