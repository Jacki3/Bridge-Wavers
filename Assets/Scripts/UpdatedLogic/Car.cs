using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEditor.Experimental;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Car : PathFollower
{
    public MMFeedbacks hornFeedbacks;
    public int scoreToAdd;
    [SerializeField]
    [Tooltip("The chance this car has to spawn (as a percentage)")]
    [Range(0.0f, 100.0f)]
    public float spawnChance = 50.0f;

    private WaveOperatedZone currentZone;

    private void OnEnable()
    {
        FirstPersonMovement.wave += WaveBack;
    }

    private void OnDisable()
    {
        FirstPersonMovement.wave -= WaveBack;
    }

    private void OnTriggerEnter(Collider other)
    {
        WaveOperatedZone waveZone = other.GetComponent<WaveOperatedZone>();
        if (waveZone != null)
        {
            currentZone = waveZone;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        WaveOperatedZone waveZone = other.GetComponent<WaveOperatedZone>();
        if (waveZone != null)
        {
            currentZone = null;
        }
    }

    public void WaveBack(string playerName)
    {
        //determine if this car is in a detector
        if(currentZone != null)
        {
            //Here we should update the car and/or zone visuals to show that we can wave at this car
            //Check wave zone corresponding waver
            if(currentZone.CheckCorrespodingZone(playerName))
            {
                //if so then beep or feedback, add score and do whatever -- score should be based on the car itself, how many in a row before player has been hit/timer and specific areas of the zone (edges = lowest; middle = highest)
                ScoreController.AddScoreStatic(scoreToAdd);
                Beep();
            }

        }
    }

    public void Beep()
    {
        hornFeedbacks?.PlayFeedbacks();
    }
}
