using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class ShowCurrentTime: MonoBehaviour
{

  public TMP_Text timeText; // The TextMeshPro object to display

  // Update is called once per frame
  void Update()
  {

    TimeSpan t = TimeSpan.FromSeconds( Timer.currentTime );
    string answer;
    if (t.Minutes > 9)
    {
        answer = string.Format("{0:D2}: {1:D2}", 
                t.Minutes, 
                t.Seconds);
    }
    else
    {
        answer = string.Format("{0:D1}: {1:D2}", 
                t.Minutes, 
                t.Seconds);
    }
    
    timeText.SetText(answer);
  }
}