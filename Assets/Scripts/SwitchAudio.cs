using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAudio : MonoBehaviour
{
    public AudioSource audioSourceA; // First audio source
    public AudioSource audioSourceB; // Second audio source
    public float fadeDuration = 3.0f; // Common fade duration for both sources

    private Coroutine fadeCoroutine; // To track the current fade coroutine

    private void OnTriggerEnter(Collider other) // Line 15
    {
        if (other.CompareTag("Player"))
        {
            // If a fade is currently in progress, stop it immediately
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine); // Stop any ongoing fade or switch
            }

            // Check which audio source is currently playing
            if (audioSourceA.isPlaying)
            {
                // If audioSourceA is playing, switch to audioSourceB
                StartSwitch(audioSourceA, audioSourceB); // Line 21
            }
            else if (audioSourceB.isPlaying)
            {
                // If audioSourceB is playing, switch to audioSourceA
                StartSwitch(audioSourceB, audioSourceA); // Line 25
            }
            else
            {
                // If neither is playing, start playing audioSourceA and fade it in
                StartFadeIn(audioSourceA); // Line 29
            }
        }
    }

    private void StartSwitch(AudioSource oldAudioSource, AudioSource newAudioSource) // Line 34
    {
        // Stop the current fade coroutine if it's running
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

        // Start the switch process
        fadeCoroutine = StartCoroutine(SwitchAudioCoroutine(oldAudioSource, newAudioSource, fadeDuration)); // Line 38
    }

    private void StartFadeIn(AudioSource audioSource) // Line 41
    {
        // Stop the current fade coroutine if it's running
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

        // Start the fade-in process
        fadeCoroutine = StartCoroutine(FadeIn(audioSource, fadeDuration)); // Line 45
    }

    // Coroutine to switch from one audio source to another
    private IEnumerator SwitchAudioCoroutine(AudioSource oldAudioSource, AudioSource newAudioSource, float duration) // Line 49
    {
        // Fade out the old audio source
        yield return StartCoroutine(FadeOut(oldAudioSource, duration)); // Line 52

        // Play the new audio source and fade it in
        newAudioSource.Play();
        yield return StartCoroutine(FadeIn(newAudioSource, duration)); // Line 55
    }

    // Coroutine to fade out an audio source
    private IEnumerator FadeOut(AudioSource audioSource, float duration) // Line 58
    {
        float startVolume = audioSource.volume; // Line 59

        while (audioSource.volume > 0f) // Line 61
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration; // Line 62
            yield return null; // Wait for the next frame
        }

        audioSource.Stop(); // Stop the audio after fading out
        audioSource.volume = startVolume; // Reset volume for the next time
    }

    // Coroutine to fade in an audio source
    private IEnumerator FadeIn(AudioSource audioSource, float duration) // Line 68
    {
        audioSource.volume = 0f; // Start volume at 0
        audioSource.Play();      // Start playing the audio

        while (audioSource.volume < 1.0f) // Line 72
        {
            audioSource.volume += Time.deltaTime / duration; // Increase volume
            yield return null; // Wait for the next frame
        }

        audioSource.volume = 1.0f; // Ensure volume is set to 1.0
    }
}
