using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestAudio : MonoBehaviour
{
    public AudioSource audioSource;  // The audio source to play
    public float fadeDuration = 5.0f; // Duration for the fade-in effect

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !audioSource.isPlaying)
        {
            StartCoroutine(FadeIn(audioSource, fadeDuration)); // Start the fade-in coroutine
        }
    }

    // Coroutine to fade in the audio over a set duration
    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        audioSource.volume = 0f;  // Set the initial volume to 0
        audioSource.Play();        // Start playing the audio

        // Gradually increase the volume
        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += Time.deltaTime / duration; // Increase volume over time
            yield return null; // Wait for the next frame
        }

        audioSource.volume = 1.0f; // Ensure the volume is set to 1.0 at the end
    }
}
