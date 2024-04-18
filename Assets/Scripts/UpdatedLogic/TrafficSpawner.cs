using UnityEngine;
using System.Collections.Generic;

public class TrafficSpawner : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab; // Prefab for the car game object
    [SerializeField] private Transform[] spawnPoints; // Array of transform points for spawning cars
    [SerializeField] private MinMax spawnRate; // Spawn rate range (implemented below)
    [SerializeField] private float safetyFactor = 2.0f; // Buffer multiplier for safe distance

    private float timer = 0.0f;
    private List<GameObject> spawnedCars = new List<GameObject>(); // List to store spawned cars

    void Start()
    {
        // Ensure there are at least 4 spawn points defined
        if (spawnPoints.Length < 4)
        {
            Debug.LogError("At least 4 spawn points required for traffic simulation.");
        }
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
        foreach (Transform spawnPoint in spawnPoints)
        {
            bool safeToSpawn = true;
            foreach (GameObject car in spawnedCars)
            {
                float distance = Vector3.Distance(spawnPoint.position, car.transform.position);
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

    private float CalculateSafeDistance(GameObject car)
    {
        float carSpeed = car.GetComponent<CarScript>().speed;  // Assuming 'CarScript' has a 'speed' variable
        float averageSpawnTime = (spawnRate.min + spawnRate.max) / 2.0f;
        return carSpeed * averageSpawnTime * safetyFactor;
    }

    private void SpawnCar()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length); // Choose a random spawn point

        // Check for safe distance again before spawning
        if (CanSpawnAtPoint(spawnPoints[spawnIndex], carPrefab))
        {
            GameObject car = Instantiate(carPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
            spawnedCars.Add(car); // Add spawned car to the list

            // Set car to its speed retrieved from the prefab
            car.GetComponent<Rigidbody>().velocity = car.transform.forward * car.GetComponent<CarScript>().speed;
        }
    }

    private bool CanSpawnAtPoint(Transform spawnPoint, GameObject carPrefab)
    {
        // Redundant check in this case (kept for potential future modifications)
        // Can be removed if car speed and spawn rate logic guarantee safe distance
        float safeDistance = CalculateSafeDistance(carPrefab);
        foreach (GameObject car in spawnedCars)
        {
            float distance = Vector3.Distance(spawnPoint.position, car.transform.position);
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
        foreach (GameObject car in spawnedCars)
        {
            Destroy(car);
        }
        spawnedCars.Clear();
    }

    // Nested MinMax Class to represent a range of values
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
