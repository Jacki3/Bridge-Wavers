using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private float countDur;

    [SerializeField]
    private float countDelay;

    [SerializeField]
    private NumberCounter counter;

    [SerializeField]
    private ScoreGainUI scoreGain;

    [SerializeField]
    private Canvas mainCanvas;

    [SerializeField]
    private Transform scoreGainSpawn;

    private static UIController i;

    public enum UITextComponent
    {
        none,
        scoreText,
        timerText
    }

    private void Awake()
    {
        i = this;
    }

    private void SpawnScoreGain(int score)
    {
        ScoreGainUI newScoreGain = Instantiate(scoreGain);
        newScoreGain
            .SetScoreGain("+ " + score, scoreGainSpawn, mainCanvas.transform);
    }

    public static void SpawnGain(int score)
    {
        i.SpawnScoreGain (score);
    }

    private void UpdateText(UITextComponent component, string text) //need to ensure you can account for any type of text holder
    {
        if (CoreUIElements.i != null)
            CoreUIElements.i.GetTextComponent(component).text = text;
    }

    public static void UpadteTextStatic(
        UITextComponent component,
        string textToAdd
    )
    {
        i.UpdateText (component, textToAdd);
    }

    public static void CountUpScore(int value)
    {
        i.counter.Value = value;
    }
}
