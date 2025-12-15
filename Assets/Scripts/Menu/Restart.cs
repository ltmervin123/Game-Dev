using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{

    public GameObject restartPanel;
    public GameObject player;

    [SerializeField] private GameOver gameOver;


    void Start()
    {

        restartPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && player == null)
        {
            RestartGame();
        }

    }

    public void ShowRestartPanel()
    {
        restartPanel.SetActive(true);
        gameOver.PlayGameOverSound();
    }



    private void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
