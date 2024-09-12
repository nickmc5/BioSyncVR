using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Container for persistent settings from the main menu
public class SessionSettings : MonoBehaviour
{
    public static int sessionLength = 5;   // either 5 mins or 10 mins

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}
