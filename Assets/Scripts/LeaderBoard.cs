using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject container;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI timeText;
    // Start is called before the first frame update
    void Start()
    {
        Scores score = HighScore.GetScores();
        for (int i = 0; i <= score._scores.Count-1; i++)
        {
            GameObject text =  Instantiate(prefab, container.transform);

            nameText = text.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            timeText = text.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            nameText.text = score._names[i];
            float time = score._scores[i];

            int minutes = Mathf.FloorToInt(time / 60f);
            int seconde = seconde = Mathf.FloorToInt(time - minutes * 60f);
            timeText.text = string.Format("{0:00} : {1:00}", minutes, seconde);
        }
    }
}
