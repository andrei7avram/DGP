using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFood : MonoBehaviour
{
    public GameObject foodPrefab;

    private bool isSpawning = false;
    void Update()
    {
        if (!foodPrefab.activeSelf && !isSpawning)
        {
            StartCoroutine(RespawnFoodCoroutine());
        }
    }

    IEnumerator RespawnFoodCoroutine()
    {
        isSpawning = true;
        yield return new WaitForSeconds(10);
        foodPrefab.SetActive(true);
        isSpawning = false;
    }
}
