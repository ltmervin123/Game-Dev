using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    void Start()
    {
        // Make sure pause panel is off initially
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        // Toggle pause with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        // Allow unpause with 'P' key as well
        if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f; // freezes game physics
        isPaused = true;
    }

    void ResumeGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f; // resumes physics
        isPaused = false;
    }
}
