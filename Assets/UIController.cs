using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    bool paused;
    bool endGame;
    public GameObject menuPanel;

    public 
    // Start is called before the first frame update
    void Start()
    {
        menuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                ResumeGame();
            else
                PauseGame();

            paused = !paused;
        }
    }

    public void OnResumeRestart()
    {
        if (!endGame)
        {
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
}
