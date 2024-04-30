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
    private GameObject pauseMenu; //this does not belong here but in a pause game manager

    [SerializeField]
    private AudioSource music; //same again - should be in a separate audio manager (even game music manager)

    private void OnEnable()
    {
        Timer.TimeEnded += EndGame; //doesnt work here , should be in a separate timer manager - we are simply listening for events and triggering states rather than controlling individual components
    }

    private void OnDisable()
    {
        Timer.TimeEnded -= EndGame; //same as above
    }

    private void Start()
    {
        StateManager.gameState = StateManager.State.Menu; //this works here
    }

    //not bad but use generic inputs - could even be handled in a separate input manager (prob should be)
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
        StateManager.gameState = StateManager.State.Playing; //all fine but remember that trigger for starting game is listend for in the timer manager
        StateManager.paused = false;
        Timer.StartTimer(levelTime);
    }

    private void EndGame()
    {
        ScoreController.SetHighScoreStatic(); //same again - should be in a separate score manager - we are simply listening for events and triggering states rather than controlling individual components
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
                pauseMenu.SetActive(true); //pause menu listens for triggers in the pause game manager
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
            pauseMenu.SetActive(false); //pause menu listens for triggers in the pause game manager
    }

    public void RestartLevel()
    {
        Pause(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //separator for level manager
    }

    public void LoadMenu()
    {
        //do fade stuff in animator -- should be in level/scene manager script
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MuteMusic() //should be in a separate audio manager
    {
        if (!music.mute)
            music.mute = true;
        else
            music.mute = false;

    }
}
