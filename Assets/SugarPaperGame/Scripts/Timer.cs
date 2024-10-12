using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private UnityEvent onEnd;

    private float t;

    private void Awake()
    {
        t = 0;
    }

    private void Update()
    {
        t += Time.deltaTime;

        if (t >= time)
        {
            onEnd?.Invoke();
            t = 0;
        }
    }
}
