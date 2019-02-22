using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Prefs
    {
    public static int HighScore
    {
        get => PlayerPrefs.GetInt(nameof(HighScore));
        set => PlayerPrefs.SetInt(nameof(HighScore), value);
    }

    static int _score;
    public static int Score
    {
        get => _score;
        set
        {
            _score = value;
            if (Score > HighScore) HighScore = Score;
        }
    }
}
