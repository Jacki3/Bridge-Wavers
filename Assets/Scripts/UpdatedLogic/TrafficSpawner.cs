using UnityEngine;
using System.Collections.Generic;
using PathCreation;
using System;
using Random = UnityEngine.Random;

// Realistically you don't. You record audio of a car idle. And then increase playback speed and sound level based on engine RPM in game -- for game engine audio
public class TrafficSpawner : MonoBehaviour
{
    [SerializeField] private Car[] cars; // Prefab for the car game object
    [SerializeField] private PathCreator[] paths; // Array of transform points for spawning cars
    [SerializeField] private MinMax spawnRate; // Spawn rate range (implemented below)
    [SerializeField] private float safetyFactor = 2.0f; // Buffer multiplier for safe distance

    private float timer = 0.0f;
    private List<Car> spawnedCars = new List<Car>(); // List to store spawned cars

    void Start()
    {
        // Ensure there are at least 4 spawn points defined
        //if (paths.Length < 4)
        //{
        //    Debug.LogError("At least 4 spawn points required for traffic simulation.");
        //}
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (CanSpawn())
        {
            SpawnCar();
            timer = 0.0f;
        }
    }

    private bool CanSpawn()
    {
        if (spawnedCars.Count == 0) // Always allow spawn if no cars exist
        {
            return true;
        }

        // Check for safe distance to last spawned car at each spawn point
        foreach (PathCreator path in paths)
        {
            bool safeToSpawn = true;
            foreach (Car car in spawnedCars)
            {
                float distance = Vector3.Distance(path.path.GetPoint(0), car.transform.position);
                if (distance < CalculateSafeDistance(car))  // Pass car object to calculate distance
                {
                    safeToSpawn = false;
                    break;
                }
            }
            if (safeToSpawn)
            {
                return true;
            }
        }
        return false;
    }

    private float CalculateSafeDistance(Car car)
    {
        float carSpeed = car.speed;  // Assuming 'CarScript' has a 'speed' variable
        float averageSpawnTime = (spawnRate.min + spawnRate.max) / 2.0f;
        return carSpeed * averageSpawnTime * safetyFactor;
    }

    //private void SpawnCar()
    //{
    //    int pathIndex = Random.Range(0, paths.Length); // Choose a random spawn point

    //    // Check for safe distance again before spawning
    //    if (CanSpawnAtPoint(paths[pathIndex].path.GetPoint(0), carPrefab))
    //    {
    //        Car car = Instantiate(carPrefab, Vector3.zero, Quaternion.identity);
    //        car.pathCreator = paths[pathIndex];
    //        spawnedCars.Add(car); // Add spawned car to the list

    //        // Set car to its speed retrieved from the prefab
    //        car.GetComponent<Rigidbody>().velocity = car.transform.forward * car.speed;
    //    }
    //}

    private void SpawnCar()
    {
        int pathIndex = Random.Range(0, paths.Length);

        // Randomly select a car prefab based on their likelihood of spawning
        Car selectedCar = ChooseRandomCarPrefab();
        if (selectedCar != null && CanSpawnAtPoint(paths[pathIndex].path.GetPoint(0), selectedCar))
        {
            Car car = Instantiate(selectedCar, Vector3.zero, Quaternion.identity);
            car.pathCreator = paths[pathIndex];
            spawnedCars.Add(car);
            car.GetComponent<Rigidbody>().velocity = car.transform.forward * car.speed;
        }
    }

    private Car ChooseRandomCarPrefab()
    {
        // Calculate total likelihood sum
        float chanceToSpawn = 0f;
        foreach (Car car in cars)
        {
            chanceToSpawn += car.spawnChance;
        }

        // Choose a random value within the total likelihood sum
        float randomValue = Random.Range(0f, chanceToSpawn);

        // Iterate through the car prefabs and accumulate their likelihood until reaching the random value
        float accumulatedChance= 0f;
        foreach (Car car in cars)
        {
            accumulatedChance += car.spawnChance;
            if (randomValue <= accumulatedChance)
            {
                return car;
            }
        }

        // This should not happen, but in case of an error, return null
        return null;
    }

    private bool CanSpawnAtPoint(Vector3 spawnPoint, Car carPrefab)
    {
        // Redundant check in this case (kept for potential future modifications)
        // Can be removed if car speed and spawn rate logic guarantee safe distance
        float safeDistance = CalculateSafeDistance(carPrefab);
        foreach (Car car in spawnedCars)
        {
            float distance = Vector3.Distance(spawnPoint, car.transform.position);
            if (distance < safeDistance)
            {
                return false;
            }
        }
        return true;
    }

    void OnDisable()
    {
        // Clean up spawned cars when script is disabled
        foreach (Car car in spawnedCars)
        {
            Destroy(car);
        }
        spawnedCars.Clear();
    }

    // Nested MinMax Class to represent a range of values
    [Serializable]
    public class MinMax
    {
        public float min;
        public float max;

        public MinMax() { }

        public MinMax(float minVal, float maxVal)
        {
            min = minVal;
            max = maxVal;
        }
    }
}
