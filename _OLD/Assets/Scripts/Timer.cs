using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private static float timeLeft;

    private static bool timerOn = false;

    public static event EndTime TimeEnded;

    public delegate void EndTime();

    public static void StartTimer(float timeToCount)
    {
        timeLeft = timeToCount;
        timerOn = true;
    }

    private void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimerText (timeLeft);
            }
            else
            {
                TimeEnded();
                timeLeft = 0;
                timerOn = false;
            }
        }
    }

    private static void UpdateTimerText(float time)
    {
        time += 1;

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string timeText = string.Format("{0:00} : {1:00}", minutes, seconds);
        UIController
            .UpadteTextStatic(UIController.UITextComponent.timerText, timeText);
    }
}
