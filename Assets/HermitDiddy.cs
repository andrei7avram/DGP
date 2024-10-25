using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitDiddy : MonoBehaviour
{
    public GameObject hermitPrefab;
    public int hermitCount = 0;
    public Vector3 spawnRange = new Vector3(1, 0, 1);

    public AudioSource audioSource;

    public AudioClip[] audioClips;
    private bool partyStarted = false;


    void Update()
    {
        if (!audioSource.isPlaying && hermitCount >0)
        {
            PlayRandomClip();
        }
    }

    void PlayRandomClip()
    {   
        float random = Random.Range(0f, 1f);
        if (random <= 0.1f)
        {
            audioSource.clip = audioClips[0]; // 10% chance to play the first audio clip
        }
        else if (random > 0.1f && random <= 0.5f)
        {
            audioSource.clip = audioClips[1]; // 40% chance to play the second audio clip
        }else
        {
            audioSource.clip = audioClips[2]; // 50% chance to play the third audio clip
        }
        audioSource.Play();
    }


    public void AddHermit() {
        Debug.Log("Hermit added");
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnRange.x, spawnRange.x),
            1,
            Random.Range(-spawnRange.z, spawnRange.z)
        );
        Vector3 spawnPosition = transform.position + randomOffset;
        Instantiate(hermitPrefab, spawnPosition, Quaternion.identity);
        hermitCount++;
    }
}
