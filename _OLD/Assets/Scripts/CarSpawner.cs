using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

namespace PathCreation
{
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<PathFollower> defaultVehicles = new List<PathFollower>();

        [SerializeField]
        private List<PathFollower> uncommonVehicles = new List<PathFollower>();

        [SerializeField]
        private List<PathFollower> rareVehicles = new List<PathFollower>();

        [SerializeField]
        private List<PathCreator> paths = new List<PathCreator>();

        [SerializeField]
        private PathCreator path;

        [SerializeField]
        private float minSpawnTime = 0;

        [SerializeField]
        private float maxSpawnTime = 0;

        [SerializeField]
        private float uncommonVehiclesRand = 0.65f;

        [SerializeField]
        private float rareVehiclesRand = 0.9f;

        [SerializeField]
        private List<CarSequence> carSequences = new List<CarSequence>();

        private float timer;

        private float spawnTime;

        [System.Serializable]
        private class CarSequence
        {
            [System.Serializable]
            private class Cars
            {
                [SerializeField]
                private Car car;

                [SerializeField]
                private Count waitTimes;
            }

            [SerializeField]
            private List<Cars> cars = new List<Cars>();

            private int count = 0;
        }

        [System.Serializable]
        public class Count
        {
            public int minimum;

            public int maximum;

            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }

        void Start()
        {
            timer = 0;
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                timer = 0;
                Spawn();
            }
        }

        void Spawn()
        {
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            int randPath = Random.Range(0, paths.Count);
            PathCreator path = paths[randPath];

            List<PathFollower> vehicleList = defaultVehicles;

            if (Random.value > rareVehiclesRand)
                vehicleList = rareVehicles;
            else if (Random.value > uncommonVehiclesRand)
                vehicleList = uncommonVehicles;

            int randCar = Random.Range(0, vehicleList.Count);
            PathFollower vehicle = vehicleList[randCar];
            PathFollower newCar =
                Instantiate(vehicle, Vector3.zero, Quaternion.identity);
            newCar.pathCreator = path;
            newCar.endOfPathInstruction = EndOfPathInstruction.Stop;
        }
    }
}
