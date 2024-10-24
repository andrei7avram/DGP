using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutSpawner : MonoBehaviour
{
    private int maxCoconuts = 3;

    private int currentCoconuts = 0;

    private int coconutSpawnChance = 75;

    public GameObject coconutPrefab;

    private Vector3 spawnPosition;
    
    public AudioClip clip;
    public AudioSource src;

    private void SpawnCocoNut()
    {   
        spawnPosition = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        if (Random.Range(0, 100) < coconutSpawnChance && currentCoconuts < maxCoconuts)
        {
            Instantiate(coconutPrefab, spawnPosition, Quaternion.identity);
            currentCoconuts++;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack") )
        {   
            Debug.Log("COCO");
            if (src != null && clip != null) { src.PlayOneShot(clip); }
            SpawnCocoNut();
        }
    }
}
