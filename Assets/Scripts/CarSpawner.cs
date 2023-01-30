using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Car> vehicles = new List<Car>();

    [SerializeField]
    private List<Vector3> spawns = new List<Vector3>();

    [SerializeField]
    private int minSpawnTime = 0;

    [SerializeField]
    private int maxSpawnTime = 0;

    private float timer;

    private int spawnTime;

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
        int randSpawn = Random.Range(0, spawns.Count);
        Vector3 spawn = spawns[randSpawn];

        int randCar = Random.Range(0, vehicles.Count);
        Car vehicle = vehicles[randCar];
        Car newCar = Instantiate(vehicle, spawn, vehicle.transform.rotation);
    }
}
