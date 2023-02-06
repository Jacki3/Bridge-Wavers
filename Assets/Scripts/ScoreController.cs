using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private static ScoreController i;

    private int totalScore;

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        UIController.UpadteTextStatic("0");
    }

    private void AddScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        UIController.CountUpScore (totalScore);
        UIController.SpawnGain (scoreToAdd);
    }

    public static void AddScoreStatic(int scoreToAdd)
    {
        i.AddScore (scoreToAdd);
    }

    private void ResetScore()
    {
        totalScore = 0;
        UIController.CountUpScore (totalScore);
        //animate
    }

    public static void ResetScoreStatic()
    {
        i.ResetScore();
    }
}
