using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Timer : MonoBehaviour
{
    float currentTime;
    bool TimerIsAlive; 
    [SerializeField]
    TextMeshProUGUI timeText;
    void Start()
    {
        TimerIsAlive = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerIsAlive)
        {
            currentTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconde = seconde = Mathf.FloorToInt(currentTime - minutes * 60f);
            timeText.text = string.Format("{0:00} : {1:00}", minutes, seconde);
        }
    }
    public void StopAndSaveTimer()
    {
        TimerIsAlive = false;
        PlayerPrefs.SetFloat("Time", currentTime);

    }
}
