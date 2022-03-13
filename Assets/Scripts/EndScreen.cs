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
            gameOverText.text += PlayerPrefs.GetString("Name"); 
            timeText.gameObject.SetActive(!timeText.IsActive());
            float currentTime = PlayerPrefs.GetFloat("Time");

            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconde  = Mathf.FloorToInt(currentTime - minutes * 60f);

            timeText.text = "Your Time is : " + string.Format("{0:00} : {1:00}", minutes, seconde);
            HighScore.SaveScore(PlayerPrefs.GetString("Name"), currentTime);
        }
    }

}
