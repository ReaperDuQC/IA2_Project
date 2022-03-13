using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Timer : MonoBehaviour
{
    float currentTime;
    [SerializeField]
    TextMeshProUGUI timeText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconde = seconde = Mathf.FloorToInt(currentTime - minutes * 60f);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconde);
    }
}
