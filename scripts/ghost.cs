using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private List<Vector3> recordedPositions = new List<Vector3>();
    private List<Quaternion> recordedRotations = new List<Quaternion>();
    private float totalTimeElapsed = 0f;
    private int currentIndex = 0;
    private float lerpDuration = 0.2f; // 200ms interval for each update (position and rotation change every 200ms)
    public string save_position;
    public string save_rotation;
    void Start()
    {
        // Retrieve and deserialize the recorded data from PlayerPrefs
        LoadRecordedData();

        // Start the playback coroutine
        StartCoroutine(PlayBack());
    }

    private void LoadRecordedData()
    {
        string positionJson = PlayerPrefs.GetString(save_position);
        string rotationJson = PlayerPrefs.GetString(save_rotation);

        if (!string.IsNullOrEmpty(positionJson) && !string.IsNullOrEmpty(rotationJson))
        {
            // Deserialize the JSON strings back to arrays
            float[] positionArray = JsonHelper.FromJson<float>(positionJson);
            float[] rotationArray = JsonHelper.FromJson<float>(rotationJson);

            // Convert the position array into Vector3
            for (int i = 0; i < positionArray.Length; i += 3)
            {
                Vector3 position = new Vector3(positionArray[i], positionArray[i + 1], positionArray[i + 2]);
                recordedPositions.Add(position);
            }

            // Convert the rotation array into Quaternion
            for (int i = 0; i < rotationArray.Length; i += 4)
            {
                Quaternion rotation = new Quaternion(rotationArray[i], rotationArray[i + 1], rotationArray[i + 2], rotationArray[i + 3]);
                recordedRotations.Add(rotation);
            }
        }
    }

    private IEnumerator PlayBack()
    {
        if (recordedPositions.Count == 0 || recordedRotations.Count == 0) yield break;

        // Play back each position and rotation with interpolation over time
        while (currentIndex < recordedPositions.Count - 1)
        {
            Vector3 startPos = recordedPositions[currentIndex];
            Quaternion startRot = recordedRotations[currentIndex];
            startRot.eulerAngles = new Vector3(startRot.eulerAngles.x, startRot.eulerAngles.y + 180f, startRot.eulerAngles.z);

            Vector3 endPos = recordedPositions[currentIndex + 1];
            Quaternion endRot = recordedRotations[currentIndex + 1];
            endRot.eulerAngles = new Vector3(endRot.eulerAngles.x, endRot.eulerAngles.y + 180f, endRot.eulerAngles.z);

            float elapsedTime = 0f;

            while (elapsedTime < lerpDuration)
            {
                // Lerp the position and rotation over the duration (200ms)
                float lerpFactor = elapsedTime / lerpDuration;

                // Smoothly interpolate position and rotation
                transform.position = Vector3.Lerp(startPos, endPos, lerpFactor);

                transform.rotation = Quaternion.Lerp(startRot, endRot, lerpFactor);

                elapsedTime += Time.deltaTime; // Increase the elapsed time
                yield return null; // Wait until the next frame
            }

            // Once the interpolation is done, set to the final target position and rotation
            transform.position = endPos;
            transform.rotation = endRot;

            currentIndex++; // Move to the next position/rotation pair
        }

        // After the loop, ensure the object ends exactly at the last position and rotation
        transform.position = recordedPositions[recordedPositions.Count - 1];
        transform.rotation = recordedRotations[recordedRotations.Count - 1];
    }
}
