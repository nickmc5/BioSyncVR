using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextFileIO : MonoBehaviour
{
    public string filepath;
    public string readData;

    public void SetFilePath() {
        filepath = "Assets/test.txt"; // hard coded file path, change later
    }

    public string ReadString() {
       //Read the text from directly from the test.txt file
       StreamReader reader = new StreamReader(filepath);
       readData = reader.ReadToEnd();
       Debug.Log(readData);
       reader.Close();
       return readData;
    }
}