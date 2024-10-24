using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypeWriter : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Use TextMeshPro for UI text or TextMeshProUGUI for canvas
    public TextMeshProUGUI textMeshPro2;
    public float delayBetweenCharacters = 0.05f; // Time between revealing characters
    private string fullText; // Store the full text
    private string currentText = ""; // Store the currently displayed text

    private int spacePressCount = 0;

    void Start()
    {
        // Get the full text from the TextMeshPro component
        fullText = textMeshPro.text;

        // Clear the text at the beginning
        textMeshPro.text = "";

        // Start the typewriter effect coroutine
        StartCoroutine(RevealText());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressCount++;
            if (spacePressCount == 1)
            {
                StopAllCoroutines();
                textMeshPro.text = fullText;

            }
            else if (spacePressCount == 2)
            {
                // Load the "World" scene on the second space press
                SceneManager.LoadScene("World");
            }
        }
    }

    IEnumerator RevealText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            // Set the text to the current substring
            currentText = fullText.Substring(0, i);
            textMeshPro.text = currentText;

            if(i == fullText.Length/5)
            {
                textMeshPro2.enabled = true;
            }

            // Wait before revealing the next character
            yield return new WaitForSeconds(delayBetweenCharacters);
        }

    }
}
