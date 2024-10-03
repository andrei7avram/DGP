using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSpawnerScript : MonoBehaviour
{

    public GameObject prefab;
    private bool spawnerStarted = false;
    public void call() {
        spawnerStarted = true;
        Debug.Log("Bomb started");
    }

    void Update() {
        if (spawnerStarted == true) {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
