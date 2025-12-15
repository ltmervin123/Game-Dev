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

    }
}
