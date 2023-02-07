using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float levelTime; //perhaps better in a separate variable script

    private void OnEnable()
    {
        Timer.TimeEnded += EndGame;
    }

    private void OnDisable()
    {
        Timer.TimeEnded -= EndGame;
    }

    private void Start()
    {
        StateManager.gameState = StateManager.State.Menu;
    }

    private void Update()
    {
        if (
            Input.GetKey(KeyCode.Space) &&
            StateManager.gameState != StateManager.State.Playing
        ) StartGame();
    }

    private void StartGame()
    {
        StateManager.gameState = StateManager.State.Playing;
        StateManager.paused = false;
        Timer.StartTimer (levelTime);
    }

    private void EndGame()
    {
        ScoreController.SetHighScoreStatic();
        ScoreController.ResetScoreStatic();
        StateManager.gameState = StateManager.State.EndGame;
        StateManager.paused = true;
    }
}
