using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    bool paused;
    bool endGame;
    public GameObject menuPanel;

    public Text resumeRestartText;
    public Text resumeRestartButton;

    public Text timerText;
    public Text scoreText;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        menuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (endGame)
            return;
        UpdateUI();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                ResumeGame();
            else
                PauseGame();

            paused = !paused;
        }
    }

    public void ConfigurePause()
    {
        resumeRestartText.text = "Pause";
        resumeRestartButton.text = "Resume";
    }

    public void ConfigureRestart()
    {
        resumeRestartText.text = "New Game?";
        resumeRestartButton.text = "Restart";
        endGame = true;
        PauseGame();
    }

    public void OnResumeRestart()
    {
        if (!endGame)
        {
            ResumeGame();
        }
        else
        {
            ConfigurePause();
            endGame = false;
            ResumeGame();
        }
    }
    public void OnExit()
    {
        Application.Quit();
    }
    void PauseGame()
    {
        Time.timeScale = 0;
        menuPanel.SetActive(true);
    }

    void ResumeGame()
    {

        Time.timeScale = 1;
        menuPanel.SetActive(false);
    }
    private void UpdateUI()
    {
        scoreText.text = $"Retrieval: {gameManager.playerLevel}";
        var formattedTime =  string.Format("{0}:{1:00}", (int)gameManager.currentRoundTimer / 60, (int)gameManager.currentRoundTimer % 60);
        timerText.text = $"Time: -{formattedTime}";
    }

}
