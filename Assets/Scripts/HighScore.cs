using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Scores
{
    public List<float> _scores;
    public List<string> _names;
    public Scores(List<float> score, List<string> name)
    {
        _scores = score;
        _names = name; 
    }
}

public class HighScore
{
    static public void SaveScore(string name, float time)
    {
        int position = CompareScore(time);
        if (position < 5)
        {
            string key = "score" + position; 
            PlayerPrefs.SetFloat(key, time);
            key = "name" + position;
            PlayerPrefs.SetString(key, name);
        }
    }

    static public Scores GetScores()
    {
        List<float> scores = new List<float>();
        List<string> names = new List<string>();

        int nbOfScores = 5;
        string baseKey = "score";
        for (int i = 0; i < nbOfScores; i++)
        {
            string key = baseKey + i;
            if (PlayerPrefs.GetFloat(key) != 0)
            {
                scores.Add(PlayerPrefs.GetFloat(key));
                key = "name" + i;
                names.Add(PlayerPrefs.GetString(key));
            }
        }

        return new Scores(scores, names);
    }

    static private int CompareScore(float time)
    {
        Scores scores = GetScores();
        int nbOfScores = 5;
        int index = scores._scores.Count;
        if (scores._scores.Count > 0)
        {
            for (int i = scores._scores.Count - 1; i >= 0; i--)
            {
                if (time < scores._scores[i])
                {
                    index = i;
                }
            }
            float nextScore;
            string nextName; 
            for (int i = nbOfScores - 1; i > index; i--)
            {
               // nextScore = PlayerPrefs.GetFloat("score" + (i + 1)); // save le prochain idx 
                //nextName = PlayerPrefs.GetString("name" + (i + 1)); // save prochain idx 

                PlayerPrefs.SetFloat("score" + (i), PlayerPrefs.GetFloat("score" + (i -1))); // efface prochain idx 
                PlayerPrefs.SetString("name" + (i), PlayerPrefs.GetString("name" + (i -1)));

                
            }
        }
        return index; 
    }

}
