using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private static ScoreController i;

    private int totalScore;

    private int highScore;

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        UIController
            .UpadteTextStatic(UIController.UITextComponent.scoreText, "0");
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

    private void SetHighScore()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            if (totalScore > PlayerPrefs.GetInt("highScore"))
            {
                highScore = totalScore;
                PlayerPrefs.SetInt("highScore", highScore);
                PlayerPrefs.Save();
            }
        }
        else
        {
            if (totalScore > highScore)
            {
                highScore = totalScore;
                PlayerPrefs.SetInt("highScore", highScore);
                PlayerPrefs.Save();
            }
        }

        string currentHighScore = PlayerPrefs.GetInt("highScore").ToString();

        UIController
            .UpadteTextStatic(UIController.UITextComponent.timerText,
            currentHighScore);
    }

    public static void SetHighScoreStatic()
    {
        i.SetHighScore();
    }
}
