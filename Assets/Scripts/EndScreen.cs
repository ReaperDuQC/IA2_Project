using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class EndScreen : MonoBehaviour
{
    bool isGameOver;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI timeText;

    private void Awake()
    {
        int x = PlayerPrefs.GetInt("GameOver");
        isGameOver = x == 1 ? true : false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void Start()
    {
        if (isGameOver)
        {
            gameOverText.text = "Game Over";
        }
        else
        {
            timeText.gameObject.SetActive(!timeText.IsActive()); 
            timeText.text = "Your Time is : "; // + currentTime.toString(); 
        }
    }

}
