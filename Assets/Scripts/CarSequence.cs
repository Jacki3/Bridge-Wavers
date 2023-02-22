using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

[System.Serializable]
public class CarSequence : MonoBehaviour
{
    [System.Serializable]
    public class Cars
    {
        public Car[] car;

        public CarSpawnerSequences.Count waitTimes;
    }

    public List<Cars> cars = new List<Cars>();

    public int count = 0;
}
