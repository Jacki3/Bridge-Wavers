using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private List<GameObject> modelPrefabs = new List<GameObject>();

    private void Start()
    {
        int randCar = Random.Range(0, modelPrefabs.Count);
        GameObject car = Instantiate(modelPrefabs[randCar], transform);

        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.size /= 2;
        collider.isTrigger = true;

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        if (transform.position.x < 0)
        {
            transform.Rotate(0, 180, 0);
        }
    }

    void Update()
    {
        transform.position +=
            transform.forward * Time.deltaTime * movementSpeed;
    }

    public virtual void WaveBack()
    {
    }
}
