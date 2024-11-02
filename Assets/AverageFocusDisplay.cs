using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Application = UnityEngine.Application;

public class AverageFocusDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI averageFocusText;
    [SerializeField] private string prefix = "Average Focus Level: "; // Configurable prefix
    private string sessionsFolderPath;
    private const int MIN_SESSION_LENGTH = 300; // 5 minutes in seconds

    private void Awake()
    {
        // Initialize as early as possible
        sessionsFolderPath = Path.Combine(Application.persistentDataPath, "sessions");
        Debug.Log($"Sessions folder path: {sessionsFolderPath}");
    }

    private void Start()
    {
        if (averageFocusText == null)
        {
            Debug.LogError("TextMeshProUGUI component not assigned to AverageFocusDisplay!");
            return;
        }

        // Force an immediate update
        UpdateAverageFocusDisplay();
    }

    public void UpdateAverageFocusDisplay()
    {
        if (averageFocusText == null)
        {
            Debug.LogError("TextMeshProUGUI component not assigned to AverageFocusDisplay!");
            return;
        }

        try
        {
            float averageFocus = CalculateLatestSessionAverage();
            // Convert to percentage and round to one decimal place
            string percentageText = $"{prefix}{(averageFocus * 100f):F1}%";
            averageFocusText.text = percentageText;
            Debug.Log($"Updated average focus display to: {percentageText}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error updating average focus display: {e.Message}");
            averageFocusText.text = $"{prefix}0.0%";
        }
    }

    private float CalculateLatestSessionAverage()
    {
        if (!Directory.Exists(sessionsFolderPath))
        {
            Debug.LogWarning($"Sessions directory not found at: {sessionsFolderPath}");
            return 0f;
        }

        string[] sessionFiles = Directory.GetFiles(sessionsFolderPath, "meditation_session_*.csv");

        if (sessionFiles.Length == 0)
        {
            Debug.Log("No session files found in: " + sessionsFolderPath);
            return 0f;
        }

        // Get the most recent file based on actual file creation time
        string mostRecentFile = sessionFiles
            .OrderByDescending(f => new FileInfo(f).CreationTime)
            .First();

        Debug.Log($"Reading most recent file: {Path.GetFileName(mostRecentFile)}");

        try
        {
            string[] lines = File.ReadAllLines(mostRecentFile);

            if (lines.Length <= 1)
            {
                Debug.LogWarning("Session file is empty or contains only header.");
                return 0f;
            }

            // Find where the summary section starts
            int summaryIndex = -1;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("\nSession Summary") || lines[i].StartsWith("Session Summary"))
                {
                    summaryIndex = i;
                    break;
                }
            }

            // Get only the data lines (exclude header and summary)
            var dataLines = summaryIndex != -1
                ? lines.Take(summaryIndex).Skip(1)
                : lines.Skip(1);

            var focusValues = dataLines
                .Where(line => !string.IsNullOrWhiteSpace(line)) // Skip empty lines
                .Select(line =>
                {
                    string[] parts = line.Split(',');
                    if (parts.Length < 2)
                    {
                        Debug.LogWarning($"Invalid line format: {line}");
                        return 0f;
                    }
                    return float.Parse(parts[1].Trim());
                })
                .ToList();

            Debug.Log($"Found {focusValues.Count} focus values in file");

            if (focusValues.Count >= MIN_SESSION_LENGTH)
            {
                float average = focusValues.Average();
                Debug.Log($"Calculated average focus: {average:F3} from {focusValues.Count} data points");
                return average;
            }
            else
            {
                Debug.Log($"Session too short: {focusValues.Count} data points");
                return 0f;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error processing file {Path.GetFileName(mostRecentFile)}: {e.Message}");
            return 0f;
        }
    }

    // Call this method whenever a new session is completed
    public void OnSessionComplete()
    {
        UpdateAverageFocusDisplay();
    }
}