using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TarretScreenSliderChanger : MonoBehaviour
{
    Slider slider;
    [SerializeField] GameObject fillBase;
    [SerializeField] GameObject fillRed;
    RectTransform fillBaseRect;
    RectTransform fillRedRect;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        fillBaseRect = GetComponent<RectTransform>();
        fillRedRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeSliderFillRed()
    {
        fillRed.SetActive(true);
        slider.fillRect = fillRedRect;
        fillBase.SetActive(false);
    }

    public void ChangeSliderFillBase()
    {
        fillBase.SetActive(true);
        slider.fillRect = fillBaseRect;
        fillRed.SetActive(false);
    }
}
