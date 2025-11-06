using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{

    public GameObject restartPanel;


    void Start()
    {

        restartPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

    }

    public void ShowRestartPanel()
    {
        restartPanel.SetActive(true);
    }



    private void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
