using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleData : MonoBehaviour
{
    public GameObject DataPanel;

    private void Awake()
    {
        DataPanel.SetActive(false);
    }
    public void ToggleDataMenu()
    {
        DataPanel.gameObject.SetActive(true);
    }
}
