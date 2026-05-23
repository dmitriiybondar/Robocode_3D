using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class StopWatch : MonoBehaviour
{
    private float currentTime;
    [SerializeField] private Text stopWatchText;
    public bool isRun;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRun) DisplayTime();
    }


    private void DisplayTime()
    {
        currentTime += Time.deltaTime;
        double seconds = Mathf.FloorToInt(currentTime % 60);
        double minutes = Mathf.FloorToInt(currentTime / 60);
        double millis = Mathf.FloorToInt((currentTime % 1) * 1000);
        stopWatchText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, millis);
    }

}
