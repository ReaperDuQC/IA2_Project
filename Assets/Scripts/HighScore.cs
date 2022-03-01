using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore 
{
    static public void SaveScore(float score)
    {

    }

    static public List<float> GetScores()
    {
        List<float> scores = new List<float>();

        int nbOfScores = 5;
        string baseKey = "score";
        for (int i = 0; i < nbOfScores; i++)
        {
            string key = baseKey + i;
            scores.Add(PlayerPrefs.GetFloat(key));
        }
        return scores;
    }

}
