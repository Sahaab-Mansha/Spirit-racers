using System.Collections;
using UnityEngine;

public class Position : MonoBehaviour
{
    private Vector3[] recordedPositions;  // Array to store recorded positions
    private int currentIndex = 0;         // Index to keep track of the position to apply
    private float timeElapsed = 0f;       // Time elapsed for playback
    private float playbackInterval = 0.2f; // Interval between each position (200ms)
    private bool isPlaying = false;       // Flag to check if playback is active

    // Start is called before the first frame update
    private void Start()
    {
        // Load the recorded positions from PlayerPrefs
        LoadRecordedPositions();

        // Optionally, you can start the playback when you need to
        StartPlayback();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isPlaying && recordedPositions.Length > 0)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= playbackInterval && currentIndex < recordedPositions.Length)
            {
                // Smoothly move towards the next position in the array
                transform.position = Vector3.Lerp(transform.position, recordedPositions[currentIndex], timeElapsed / playbackInterval);
                timeElapsed = 0f; // Reset time elapsed after applying a position
                currentIndex++;  // Move to the next position in the array
            }

            // Stop playback if we've gone through all positions
            if (currentIndex >= recordedPositions.Length)
            {
                isPlaying = false;
            }
        }
    }

    // Start the playback process
    public void StartPlayback()
    {
        isPlaying = true;
        currentIndex = 0;  // Start from the first recorded position
        timeElapsed = 0f;  // Reset time
    }

    // Load the recorded positions from PlayerPrefs
    private void LoadRecordedPositions()
    {
        // Retrieve the positions from PlayerPrefs (assuming they were serialized)
        if (PlayerPrefs.HasKey("best_positions"))
        {
            string positionsString = PlayerPrefs.GetString("best_positions");

            // Deserialize the positions (this assumes the positions were serialized into a string before saving)
            recordedPositions = DeserializePositions(positionsString);
        }
        else
        {
            Debug.LogWarning("No recorded positions found in PlayerPrefs.");
        }
    }

    // Method to deserialize the positions from a string
    private Vector3[] DeserializePositions(string serializedData)
    {
        // Assuming the positions were saved as a comma-separated string like "x1,y1,z1;x2,y2,z2;..."
        string[] positionStrings = serializedData.Split(';');
        Vector3[] positions = new Vector3[positionStrings.Length];

        for (int i = 0; i < positionStrings.Length; i++)
        {
            string[] coords = positionStrings[i].Split(',');

            if (coords.Length == 3)
            {
                positions[i] = new Vector3(float.Parse(coords[0]), float.Parse(coords[1]), float.Parse(coords[2]));
            }
        }

        return positions;
    }
}
