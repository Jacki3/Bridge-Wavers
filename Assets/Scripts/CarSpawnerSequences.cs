using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

namespace PathCreation
{
    public class CarSpawnerSequences : MonoBehaviour
    {
        [SerializeField]
        private PathCreator path;

        [SerializeField]
        private Count spawntimes;

        [SerializeField]
        private List<CarSequence> carSequences = new List<CarSequence>();

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
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            yield return new WaitForSeconds(Random
                        .Range(spawntimes.minimum, spawntimes.maximum));
            int randSequenceIndex = Random.Range(0, carSequences.Count);
            CarSequence newSequence = carSequences[randSequenceIndex];
            newSequence.count = 0;

            foreach (CarSequence.Cars cars in newSequence.cars)
            {
                Car newCar =
                    Instantiate(newSequence.cars[newSequence.count].car);
                newCar.pathCreator = path;
                newCar.endOfPathInstruction = EndOfPathInstruction.Stop;
                int waitTime =
                    Random
                        .Range(newSequence
                            .cars[newSequence.count]
                            .waitTimes
                            .minimum,
                        newSequence.cars[newSequence.count].waitTimes.maximum);

                if (newSequence.count < newSequence.cars.Count - 1)
                {
                    newSequence.count++;
                    yield return new WaitForSeconds(waitTime);
                }
            }

            StartCoroutine(Spawn());
        }
    }
}
