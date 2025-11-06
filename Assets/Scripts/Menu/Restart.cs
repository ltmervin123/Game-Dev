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
        if (Input.GetKeyDown(KeyCode.K))
        {
            restartPanel.SetActive(true);
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

    }



    private void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
