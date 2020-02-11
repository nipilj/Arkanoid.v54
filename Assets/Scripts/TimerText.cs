using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    private float timer = 5f;
    private Text TimeText;

    void Start()
    {
        TimeText = GetComponent<Text>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        TimeText.text = timer.ToString("f0");

        if (timer < 0)
        {
           
            timer = 5f;
        }
    }
}

