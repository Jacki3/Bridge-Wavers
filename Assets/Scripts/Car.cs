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

    public void WaveBack()
    {
        Beep();
    }

    public void Beep()
    {
        hornFeedbacks?.PlayFeedbacks();
        //should we not be adding score here using the score controller? Not only this but the method should be inheritable by children cars! -- why is this method being called by above? Seems pointless.
    }
}
