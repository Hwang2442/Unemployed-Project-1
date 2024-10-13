using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private UnityEvent onEnd;

    private float t;
    private Slider slider;
    private TextMeshProUGUI text;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        
        t = 0;
        SetTime();
    }

    private void Update()
    {
        t += Time.deltaTime;
        SetTime();

        if (t >= time)
        {
            onEnd?.Invoke();
            t = 0;
        }
    }

    private void SetTime()
    {
        float remain = time - t;
        slider.SetValueWithoutNotify(Mathf.Clamp01(remain / time));

        int s = Mathf.FloorToInt(remain);
        float ms = remain - s;
        string outputText = string.Format("{0:D2} : {1}", s, ms.ToString("N2").Substring(2));
        text.SetText(outputText);
    }
}
