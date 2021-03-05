using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TarretScreenSliderChanger : MonoBehaviour
{
    Slider slider;
    [SerializeField] GameObject fillBase;
    [SerializeField] GameObject fillRed;
    Image fillBaseImg;
    Image fillRedImg;
    RectTransform fillBaseRect;
    RectTransform fillRedRect;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        fillBaseRect = fillBase.GetComponent<RectTransform>();
        fillRedRect = fillRed.GetComponent<RectTransform>();

        fillBaseImg = fillBase.GetComponent<Image>();
        fillRedImg = fillRed.GetComponent<Image>();

        slider.fillRect = fillBaseRect;
    }

    public void ChangeSliderFillRed()
    {
        fillRedImg.enabled = true;
        slider.fillRect = fillRedRect;
        fillBaseImg.enabled = false;
    }

    public void ChangeSliderFillBase()
    {
        fillBaseImg.enabled = true;
        slider.fillRect = fillBaseRect;
        fillRedImg.enabled = false;
    }
}
