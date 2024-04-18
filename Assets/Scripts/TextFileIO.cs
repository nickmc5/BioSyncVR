using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextFileIO : MonoBehaviour
{
    public string filepath;
    public string readData;

    void Start() {
        filepath = "Assets/test.txt";
    }

    public string ReadString() {
       string path = filepath;
       //Read the text from directly from the test.txt file
       StreamReader reader = new StreamReader(path);
       readData = reader.ReadToEnd();
       Debug.Log(readData);
       reader.Close();
       return readData;
    }
}
