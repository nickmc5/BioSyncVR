using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Debug = UnityEngine.Debug;
using Application = UnityEngine.Application;

public class MeditationGraphVisualizer : MonoBehaviour
{
    public GameObject linePrefab; // Assign a line renderer prefab in inspector
    public float graphWidth = 5f;
    public float graphHeight = 3f;
    public Color lineColor = Color.blue;
    public float lineWidth = 0.1f;

    void Start()
    {
        // Get the most recent session file
        string sessionsPath = Path.Combine(Application.persistentDataPath, "sessions");
        if (!Directory.Exists(sessionsPath))
        {
            Debug.LogError("Sessions directory not found!");
            return;
        }

        string[] files = Directory.GetFiles(sessionsPath, "meditation_session_*.csv");
        if (files.Length == 0)
        {
            Debug.LogError("No session files found!");
            return;
        }

        // Sort by file creation time and get the most recent
        string mostRecentFile = files
            .OrderByDescending(f => File.GetCreationTime(f))
            .First();

        VisualizeSession(mostRecentFile);
    }

    public void VisualizeSession(string filePath)
    {
        List<Vector2> dataPoints = new List<Vector2>();

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            bool readingData = true;

            // Skip header line and process until summary section
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]) || lines[i].StartsWith("Session Summary"))
                {
                    readingData = false;
                    continue;
                }

                if (readingData)
                {
                    string[] values = lines[i].Split(',');
                    if (values.Length >= 2)
                    {
                        if (float.TryParse(values[0], out float timeStamp) &&
                            float.TryParse(values[1], out float focusLevel))
                        {
                            dataPoints.Add(new Vector2(timeStamp, focusLevel));
                        }
                    }
                }
            }

            CreateGraph(dataPoints);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error reading session file: {e.Message}");
        }
    }

    void CreateGraph(List<Vector2> dataPoints)
    {
        if (dataPoints.Count < 2) return;

        // Create line renderer
        GameObject lineObj = Instantiate(linePrefab, transform.position, Quaternion.identity, transform);
        LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();

        // Configure line renderer
        lineRenderer.positionCount = dataPoints.Count;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.useWorldSpace = true;

        // Make sure the material is configured for lines
        Material lineMaterial = new Material(Shader.Find("Sprites/Default"));
        lineMaterial.color = lineColor;
        lineRenderer.material = lineMaterial;

        // Focus level is typically between 0 and 1
        float maxY = 1f;
        float maxX = dataPoints.Max(p => p.x);

        // Create an array to hold all positions
        Vector3[] positions = new Vector3[dataPoints.Count];

        // Calculate all positions
        for (int i = 0; i < dataPoints.Count; i++)
        {
            // Normalize the point to our graph dimensions
            float normalizedX = (dataPoints[i].x / maxX) * graphWidth - (graphWidth / 2f); // Center the graph
            float normalizedY = dataPoints[i].y * graphHeight;
            positions[i] = transform.position + new Vector3(normalizedX, normalizedY, 0);
        }

        // Set all positions at once
        lineRenderer.SetPositions(positions);

        Debug.Log($"Created graph with {dataPoints.Count} points. Max time: {maxX}s");
    }
}