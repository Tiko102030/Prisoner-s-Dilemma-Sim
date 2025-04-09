using UnityEngine;
using UnityEngine.UI;

public class SpeedChanger : MonoBehaviour
{
    public Slider timeSpeedSlider;

    public float timeSpeed = 1f;

    void Start()
    {
        timeSpeed = 1f;
    }

    public void UpdateTimeSpeed()
    {
        timeSpeed = timeSpeedSlider.value;
    }

    void Update()
    {
        Time.timeScale = timeSpeed;
    }
}
