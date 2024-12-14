using UnityEngine;
using UnityEngine.UI;  // For UI elements

public class TimeRemaining : MonoBehaviour
{
    [SerializeField] private Text timeText;        // Reference to the UI Text element for displaying time
    [SerializeField] private Text bestTimeText;    // Reference to the UI Text element for displaying best time
    public string Time_saved;
    private float timeElapsed = 0f;    // Time elapsed since the game started
    private float bestTime = 0f;       // Best time saved in PlayerPrefs

    // Start is called before the first frame update
    void Start()
    {
        // Load best time from PlayerPrefs (if exists)
        bestTime = PlayerPrefs.GetFloat(Time_saved, Mathf.Infinity);
        bestTime=Mathf.Round(bestTime * 100f) / 100f;
        Debug.Log(bestTime);// Defaults to "infinity" if no BestTime is saved
    }

    // Update is called once per frame
    void Update()
    {
        // Update elapsed time
        timeElapsed += Time.deltaTime;

        // Display elapsed time in the format "Time: XX.XX"
        timeText.text = "Time: " + timeElapsed.ToString("F2");

        // Display best time in the format "Best Time: XX.XX"
        if (bestTime == Mathf.Infinity)
        {
            bestTimeText.text = "Best Time: --";  // If no best time is recorded, display "--"
        }
        else
        {
            Debug.Log("in it");
            bestTimeText.text = "Best Time: " + bestTime.ToString("F2");
        }
    }
}
