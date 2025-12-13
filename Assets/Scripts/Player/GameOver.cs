using UnityEngine;

public class GameOver : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGameOverSound()
    {
        audioSource.Play();
        Debug.Log("Game Over Sound Played");
        Debug.Log("Audio Source: " + audioSource.clip.name);
    }
}
