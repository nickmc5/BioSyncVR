using UnityEngine;
using TMPro; // Using TextMeshPro instead of legacy UI.Text
using System.IO;
using System.Linq;

public class SessionCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sessionCountText;
    private string sessionsFolderPath;
    private const int MIN_SESSION_LENGTH = 300; // 5 minutes * 60 seconds

    private void Start()
    {
        // Get the same sessions folder path as SessionRecorder
        sessionsFolderPath = Path.Combine(Application.persistentDataPath, "sessions");
        UpdateSessionCount();
    }

    public void UpdateSessionCount()
    {
        if (sessionCountText == null)
        {
            Debug.LogError("Text component not assigned to SessionCounter!");
            return;
        }

        try
        {
            int longSessions = CountLongSessions();
            sessionCountText.text = longSessions.ToString();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error updating session count: {e.Message}");
            sessionCountText.text = "Error";
        }
    }

    private int CountLongSessions()
    {
        if (!Directory.Exists(sessionsFolderPath))
        {
            Debug.LogWarning($"Sessions directory not found at: {sessionsFolderPath}");
            return 0;
        }

        int count = 0;
        string[] sessionFiles = Directory.GetFiles(sessionsFolderPath, "meditation_session_*.csv");

        foreach (string filePath in sessionFiles)
        {
            try
            {
                // Read all lines except the summary section
                string[] lines = File.ReadAllLines(filePath)
                    .TakeWhile(line => !line.StartsWith("\nSession Summary"))
                    .ToArray();

                // Subtract 1 for the header line
                int dataPoints = lines.Length - 1;

                // Since we record every second, dataPoints equals seconds
                if (dataPoints >= MIN_SESSION_LENGTH)
                {
                    count++;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"Error reading file {Path.GetFileName(filePath)}: {e.Message}");
                continue;
            }
        }

        return count;
    }

    // Call this method whenever a new session is completed
    public void OnSessionComplete()
    {
        UpdateSessionCount();
    }
}