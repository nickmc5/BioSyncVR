using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField]
    private SessionSettings sessionSettings;
    public Slider slider;

    void Update() {
        SessionSettings.sessionLength = 5 * (int) slider.value;
    }

}
