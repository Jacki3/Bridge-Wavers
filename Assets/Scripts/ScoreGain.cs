using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class ScoreGain : MonoBehaviour
{
    public TextMesh scoreText;

    public float _lifeTime = 3;

    private float spawnTime;

    private float lifeTime = 0;

    private Vector3 ascenionRate = Vector3.up;

    private void Update()
    {
        var progress = (Time.time - spawnTime) / lifeTime;

        if (progress < 1)
        {
            transform.position += ascenionRate * Time.deltaTime * 1;

            var textColour = scoreText.color;
            textColour.a = 1 - progress;
            scoreText.color = textColour;
        }
    }

    public void SetScoreGain(string text, Vector3 spawnPos, Quaternion rot)
    {
        spawnTime = Time.time;
        lifeTime = _lifeTime;

        transform.position = spawnPos;
        transform.rotation = rot;
        scoreText.text = text;
    }
}
