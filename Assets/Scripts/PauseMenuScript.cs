using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused = false;
    public Movement movementRef;
    public GameObject inputManagerRef;


    void Start()
    {
        movementRef = GameObject.Find("Player").GetComponent<Movement>();
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
        pauseMenu.GetComponent<Canvas>().enabled = true;
        movementRef.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inputManagerRef.SetActive(false);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void resumeGame() {
        Debug.Log("Resuming game");
        movementRef.enabled = true;
        inputManagerRef.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.GetComponent<Canvas>().enabled = false;
    }

    public void quitGame() {
        Debug.Log("Quitting game");
        Cursor.visible = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
