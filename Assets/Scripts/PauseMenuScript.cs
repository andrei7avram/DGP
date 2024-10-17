using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused = false;
    public Movement movementRef;
    public GameObject inputManagerRef;
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            if (!isPaused) {
                pauseGame();
            } else {
                resumeGame();
            }
        }
    }
    public void pauseGame() {
        pauseMenu.SetActive(true);
        movementRef.enabled = false;
        inputManagerRef.SetActive(false);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void resumeGame() {
        pauseMenu.SetActive(false);
        movementRef.enabled = true;
        inputManagerRef.SetActive(true);
        isPaused = false;
        Time.timeScale = 1f;
    }
}
