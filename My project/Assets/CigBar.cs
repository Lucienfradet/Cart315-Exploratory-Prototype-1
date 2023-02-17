using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CigBar : MonoBehaviour
{

    public Slider slider;

    public void setMaxNicotine(int number)
    {
        slider.maxValue = number;
    }

    public void setNicotine(int number)
    {
        slider.value = number;
    }
}
