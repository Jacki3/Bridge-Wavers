using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class NumberCounter : MonoBehaviour
{
    public string numberFormat = "N0";

    public int countFPS = 30;

    public int duration = 1;

    private int _value;

    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            UpdateText (value);
            _value = value;
        }
    }

    private Coroutine countingRoutine;

    private void UpdateText(int newValue)
    {
        if (countingRoutine != null)
        {
            StopCoroutine (countingRoutine);
        }
        countingRoutine = StartCoroutine(CountText(newValue));
    }

    private IEnumerator CountText(int newValue)
    {
        WaitForSeconds wait = new WaitForSeconds(1f / countFPS);
        int previousValue = _value;

        int step;

        if (newValue - previousValue < 0)
        {
            step =
                Mathf
                    .FloorToInt((newValue - previousValue) /
                    (countFPS * duration));
        }
        else
        {
            step =
                Mathf
                    .CeilToInt((newValue - previousValue) /
                    (countFPS * duration));
        }

        if (previousValue < newValue)
        {
            while (previousValue < newValue)
            {
                previousValue += step;
                if (previousValue > newValue)
                {
                    previousValue = newValue;
                }

                // text.SetText(previousValue.ToString(numberFormat));
                UIController
                    .UpadteTextStatic(previousValue.ToString(numberFormat));

                yield return wait;
            }
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += step;
                if (previousValue < newValue)
                {
                    previousValue = newValue;
                }

                UIController
                    .UpadteTextStatic(previousValue.ToString(numberFormat));

                yield return wait;
            }
        }
    }
}
