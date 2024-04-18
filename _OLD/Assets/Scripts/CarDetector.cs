using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDetector : MonoBehaviour
{
    [SerializeField]
    private int detectorScore;

    private bool hasCar;

    private Car currentCar;

    private void OnTriggerEnter(Collider other)
    {
        Car newCar = other.GetComponent<Car>();
        if (newCar != null)
        {
            hasCar = true;
            currentCar = newCar;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Car newCar = other.GetComponent<Car>();
        if (newCar != null)
        {
            hasCar = false;
            currentCar = null;
        }
    }

    public bool HasCar() => hasCar;

    public int GetScore() => detectorScore + currentCar.scoreToAdd;

    public Car GetCar() => currentCar;

    void Start()
    {
    }

    void Update()
    {
    }
}
