using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSliderScript : MonoBehaviour
{
    public Color minColor = Color.green;
    public Color maxColor = Color.red;

    public Image fillImage;

    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }



    public void SetSliderPowerColors()
    {
        fillImage.color = Color.Lerp(minColor, maxColor, slider.value / slider.maxValue);
    }
}
