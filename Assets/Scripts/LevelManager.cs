using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public MMFeedbacks startFeedback;
    private void Start()
    {
        startFeedback?.PlayFeedbacks();
    }
}
