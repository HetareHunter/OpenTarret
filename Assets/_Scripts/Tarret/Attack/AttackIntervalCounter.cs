using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackIntervalCounter : MonoBehaviour
{
    float attackIntervalTime = 1.4f;
    [SerializeField] GameObject screenLeftSliderObj;
    [SerializeField] GameObject screenRightSliderObj;
    Slider screenLeftSlider;
    Slider screenRightSlider;
    BaseTarretAttack tarretAttack;
    public bool countStart = false;

    float m_time = 0;
    // Start is called before the first frame update
    void Start()
    {
        screenLeftSlider = screenLeftSliderObj.GetComponent<Slider>();
        screenRightSlider = screenRightSliderObj.GetComponent<Slider>();
        tarretAttack = GetComponent<BaseTarretAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countStart)
        {
            Counter();
        }
    }

    void Counter()
    {
        m_time += Time.deltaTime;
        screenLeftSlider.value = Mathf.InverseLerp(attackIntervalTime, 0, m_time);
        screenRightSlider.value = Mathf.InverseLerp(attackIntervalTime, 0, m_time);
        if (m_time >= attackIntervalTime)
        {
            m_time = 0;
            countStart = false;
            tarretAttack.EndAttack();
        }

    }
}
