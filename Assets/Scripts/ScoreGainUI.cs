using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class ScoreGainUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    public float _lifeTime;

    private float spawnTime;

    private float lifeTime;

    private Vector3 ascenionRate = Vector3.up;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        float progress = (Time.time - spawnTime) / lifeTime;

        if (progress < 1)
        {
            transform.position += ascenionRate * Time.deltaTime * 150;

            Color textColour = text.color;
            textColour.a = 1 - progress;
            text.color = textColour;
        }
    }

    public void SetScoreGain(string _text, Transform spawnPos, Transform parent)
    {
        spawnTime = Time.time;
        lifeTime = _lifeTime;

        transform.SetParent (parent);
        transform.position = spawnPos.position;
        text.text = _text;
    }
}
