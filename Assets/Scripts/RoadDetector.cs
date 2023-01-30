using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadDetector : MonoBehaviour
{
    [SerializeField]
    private int detectorScore;

    private bool hasCar;

    private Car car;

    private void OnTriggerEnter(Collider other)
    {
        hasCar = true;
        car = other.transform.parent.GetComponent<Car>();
    }

    private void OnTriggerExit(Collider other)
    {
        hasCar = false;
        car = null;
    }

    public int GetCarScore()
    {
        if (car != null)
            return car.scoreToAdd + detectorScore;
        else
            return 0;
    }

    public bool HasCar() => hasCar;

    public Car CurrentCar() => car;
}
