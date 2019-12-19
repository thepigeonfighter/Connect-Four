using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedController : MonoBehaviour {

    public Slider slider;

    private void Start()
    {
        Time.timeScale = slider.value;
    }
    public void UpdateSpeed(Slider speedSlider)
    {
        Time.timeScale = speedSlider.value;
    }
}
