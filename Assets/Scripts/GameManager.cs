using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float levelTime; //perhaps better in a separate variable script
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private AudioSource music;

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
            Input.GetButtonUp("Wave") &&
            StateManager.gameState != StateManager.State.Playing && !StateManager.paused
        ) StartGame();

        if (Input.GetButtonUp("Escape"))
            Pause(true);

        if (Input.GetKeyUp(KeyCode.P))
            Pause(false);

    }

    private void StartGame()
    {
        StateManager.gameState = StateManager.State.Playing;
        StateManager.paused = false;
        Timer.StartTimer(levelTime);
    }

    private void EndGame()
    {
        ScoreController.SetHighScoreStatic();
        ScoreController.ResetScoreStatic();
        StateManager.gameState = StateManager.State.EndGame;
        StateManager.paused = true;
    }

    public void Pause(bool showMenu)
    {
        if (Time.timeScale > 0.0f)
        {
            Time.timeScale = 0;
            StateManager.gameState = StateManager.State.Paused;
            StateManager.paused = true;
            if (pauseMenu != null)
                pauseMenu.SetActive(true);
        }
        else
        {
            UnPause();
        }
    }

    private void UnPause()
    {
        Time.timeScale = 1;
        StateManager.gameState = StateManager.State.Playing;
        StateManager.paused = false;
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    public void RestartLevel()
    {
        Pause(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        //do fade stuff in ienumerator
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MuteMusic()
    {
        if (!music.mute)
            music.mute = true;
        else
            music.mute = false;

    }
}
