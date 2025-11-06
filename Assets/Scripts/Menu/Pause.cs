using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    void Start()
    {

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        if (pausePanel != null) pausePanel.SetActive(true);


        Time.timeScale = 0f;
        isPaused = true;
    }

    void ResumeGame()
    {
        if (pausePanel != null) pausePanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }
}
