using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowText : MonoBehaviour
{
    public string textValue;
    [SerializeField] public TextMeshProUGUI textElement;
    [SerializeField] public TextFileIO fileSource;

    void Start() {
        fileSource.SetFilePath();
        fileSource.ReadString();
        textValue = fileSource.readData;
    }

    // Update is called once per frame
    void Update() {
        textElement.text = textValue;
    }
}