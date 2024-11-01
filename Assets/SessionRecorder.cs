using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using Application = UnityEngine.Application;

public class SessionRecorder : MonoBehaviour
{
    private string sessionsFolderPath;
    private string currentSessionPath;
    private bool isRecording = false;
    private List<SessionDataPoint> sessionData;
    private float recordingInterval = 1.0f; // Record every 1000ms
    private float nextRecordTime = 0f;
    [SerializeField] private FocusManager focusManager;

    [Serializable]
    private class SessionDataPoint
    {
        public float timeStamp;
        public float focusLevel;

        public SessionDataPoint(float time, float focus)
        {
            timeStamp = time;
            focusLevel = focus;
        }
    }

    private void Awake()
    {
        // Create sessions directory in persistent data path
        sessionsFolderPath = Path.Combine(Application.persistentDataPath, "sessions");
        try
        {
            if (!Directory.Exists(sessionsFolderPath))
            {
                Directory.CreateDirectory(sessionsFolderPath);
            }
            Debug.Log($"Sessions will be saved to: {sessionsFolderPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to create sessions directory: {e.Message}");
        }

        sessionData = new List<SessionDataPoint>();
    }

    private void Start()
    {
        if (focusManager == null)
        {
            focusManager = FocusManager.Instance;
            if (focusManager == null)
            {
                Debug.LogError("FocusManager not found. Please assign it in the inspector or ensure it exists in the scene.");
                return;
            }
        }
        StartRecording();
    }

    private void Update()
    {
        if (focusManager == null)
        {
            Debug.LogError("FocusManager reference lost. Stopping recording.");
            StopRecording();
            return;
        }

        if (isRecording && Time.time >= nextRecordTime)
        {
            RecordDataPoint();
            nextRecordTime = Time.time + recordingInterval;
        }

        // Check if session has ended
        if (isRecording && Timer.currentTime <= 0f)
        {
            StopRecording();
        }
    }

    private void StartRecording()
    {
        if (focusManager == null)
        {
            Debug.LogError("Cannot start recording: FocusManager not found");
            return;
        }

        try
        {
            sessionData.Clear();
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            currentSessionPath = Path.Combine(sessionsFolderPath, $"meditation_session_{timestamp}.csv");

            // Write CSV header
            File.WriteAllText(currentSessionPath, "TimeStamp,FocusLevel\n");

            isRecording = true;
            nextRecordTime = Time.time;

            Debug.Log($"Started recording session to: {currentSessionPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to start recording: {e.Message}");
            isRecording = false;
        }
    }

    private void RecordDataPoint()
    {
        if (!isRecording || focusManager == null) return;

        try
        {
            float elapsedTime = Timer.targetTime - Timer.currentTime;
            float currentFocus = focusManager.GetFocusLevel();  // Using direct reference instead of Instance

            // Store in memory
            sessionData.Add(new SessionDataPoint(elapsedTime, currentFocus));

            // Append to file
            using (StreamWriter writer = File.AppendText(currentSessionPath))
            {
                writer.WriteLine($"{elapsedTime:F2},{currentFocus:F3}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to record data point: {e.Message}");
        }
    }

    private void StopRecording()
    {
        if (!isRecording) return;

        try
        {
            isRecording = false;

            // Calculate and append session summary
            if (sessionData.Count > 0)
            {
                float averageFocus = 0f;
                float maxFocus = float.MinValue;
                float minFocus = float.MaxValue;

                foreach (var dataPoint in sessionData)
                {
                    averageFocus += dataPoint.focusLevel;
                    maxFocus = Mathf.Max(maxFocus, dataPoint.focusLevel);
                    minFocus = Mathf.Min(minFocus, dataPoint.focusLevel);
                }
                averageFocus /= sessionData.Count;

                using (StreamWriter writer = File.AppendText(currentSessionPath))
                {
                    writer.WriteLine("\nSession Summary");
                    writer.WriteLine($"Duration: {Timer.targetTime:F1} seconds");
                    writer.WriteLine($"Average Focus: {averageFocus:F3}");
                    writer.WriteLine($"Peak Focus: {maxFocus:F3}");
                    writer.WriteLine($"Minimum Focus: {minFocus:F3}");
                    writer.WriteLine($"Data Points: {sessionData.Count}");
                }
            }

            Debug.Log($"Session recording saved to: {currentSessionPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to stop recording: {e.Message}");
        }
    }

    private void OnDestroy()
    {
        if (isRecording)
        {
            StopRecording();
        }
    }
}