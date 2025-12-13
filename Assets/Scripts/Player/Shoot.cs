using UnityEngine;

public class Shoot : MonoBehaviour
{
    public AudioClip fireSFX;
    private AudioSource audioSource;
    public float audioStartTime = 0f;
    public float audioDuration = 0.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && fireSFX != null && audioSource.clip == null)
        {
            audioSource.clip = fireSFX;

        }

    }

    public void StartAudioFire()
    {
        audioSource.time = audioStartTime;
        audioSource.Play();
        StartCoroutine(StopAfterDuration());
    }

    private System.Collections.IEnumerator StopAfterDuration()
    {
        yield return new WaitForSeconds(audioDuration);
        if (audioSource != null)
            audioSource.Stop();
    }

    public void StopAudioFire()
    {
        // if (audioSource != null)
        //     audioSource.Stop();
    }
}
