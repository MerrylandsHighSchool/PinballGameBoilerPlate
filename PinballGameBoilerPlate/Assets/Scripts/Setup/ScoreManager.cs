using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    public static int CurrentScore
    {
        get;
        private set;
    }
    public static int HighScore
    {
        get;
        private set;
    }

    public static void AddPoints(int points)
    {
        CurrentScore += points;
        if(CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
        }
    }

    public static void ResetPoints()
    {
        CurrentScore = 0;
    }
}
