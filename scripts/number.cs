using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;  // UI Text for countdown display
    [SerializeField] private AudioClip countdownSound;  // AudioClip for the countdown sound
    private AudioSource audioSource;  // AudioSource to play the sound

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();  // Adding AudioSource dynamically
        audioSource.loop = false;  // Ensure the sound doesn't loop
        audioSource.clip = countdownSound;  // Assign the countdown sound to the AudioSource

        // Freeze the game and start countdown
        Time.timeScale = 0f;  // Freeze game time
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        // Play the countdown sound
        audioSource.Play();

        // Display the countdown and wait for 1 second between each number
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);  // Wait in real-time while the game is frozen

        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);

        // After countdown is done, unfreeze the game
        StartGame();
    }

    private void StartGame()
    {
        // Unfreeze the game by setting time scale to 1 (normal speed)
        Time.timeScale = 1f;

        // Log a message to confirm the game has started
        Debug.Log("Game Started!");

        
    // Stop the countdown sound after the countdown is complete
    countdownText.gameObject.SetActive(false);
        audioSource.Stop();
    }
}
