using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Recorder : MonoBehaviour
{
    private List<Vector3> recordedPositions = new List<Vector3>();
    private List<Quaternion> recordedRotations = new List<Quaternion>();
    private float timeElapsed = 0f;
    private bool isRecording = true;
    public RawImage you_win;
    public RawImage you_lose;
    public Button btn;
    // The time interval at which to record position and rotation (200ms)
    private float recordInterval = 0.2f;
    public string save_time;
    public string save_position;
    public string save_rotation;    
    void Start()
    {
       //PlayerPrefs.DeleteAll();
        // Start recording position and rotation every 200ms
        StartCoroutine(RecordPositionAndRotation());
    }

    void Update()
    {
        if (isRecording)
        {
            timeElapsed += Time.deltaTime;
        }
    }

    private IEnumerator RecordPositionAndRotation()
    {
        while (isRecording)
        {
            // Record position and rotation at intervals of 200ms
            recordedPositions.Add(transform.position);
            recordedRotations.Add(transform.rotation);

            // Wait for 200ms
            yield return new WaitForSeconds(recordInterval);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object collided with an "End" tagged object
        if (collision.gameObject.CompareTag("End"))
        {
            // Check if there is a previously stored timeElapsed and if the new timeElapsed is greater
            if (PlayerPrefs.HasKey(save_time) && PlayerPrefs.GetFloat(save_time) < timeElapsed)
            {
                // If the previously stored timeElapsed is greater or equal, don't save new data
                Debug.Log("Not saving data because the previous timeElapsed is less or equal.");
                you_lose.gameObject.SetActive(true);
            }
            else
            {
                // Save the entire recorded data (positions, rotations, and time) to PlayerPrefs
                SaveRecordedData();
                you_win.gameObject.SetActive(true);
            }
            btn.gameObject.SetActive(true);
            // Stop recording
            isRecording = false;
        }
    }


    private void SaveRecordedData()
    {
        // Convert recorded positions and rotations to arrays of floats
        List<float> positionList = new List<float>();
        List<float> rotationList = new List<float>();

        foreach (var pos in recordedPositions)
        {
            positionList.Add(pos.x);
            positionList.Add(pos.y);
            positionList.Add(pos.z);
        }

        foreach (var rot in recordedRotations)
        {
            rotationList.Add(rot.x);
            rotationList.Add(rot.y);
            rotationList.Add(rot.z);
            rotationList.Add(rot.w);
        }

        // Convert the position and rotation lists to JSON format
        string positionJson = JsonHelper.ToJson(positionList.ToArray());
        string rotationJson = JsonHelper.ToJson(rotationList.ToArray());

        Debug.Log(positionJson + ", " + rotationJson);

        // Save the JSON strings to PlayerPrefs
        PlayerPrefs.SetString(save_position, positionJson);
        PlayerPrefs.SetString(save_rotation, rotationJson);
        PlayerPrefs.SetFloat(save_time, timeElapsed);
        
    }
}

public static class JsonHelper
{
    // Helper method to convert arrays to JSON
    public static string ToJson<T>(T[] array)
    {
        return "[" + string.Join(",", array) + "]";
    }

    // Helper method to convert JSON back to arrays
    public static T[] FromJson<T>(string json)
    {
        string[] elements = json.Trim('[', ']').Split(',');
        T[] array = new T[elements.Length];
        for (int i = 0; i < elements.Length; i++)
        {
            array[i] = (T)System.Convert.ChangeType(elements[i], typeof(T));
        }
        return array;
    }
}
