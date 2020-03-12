using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text CurrentScoretext;
    public Text HighScoretext;

    private void Start()
    {
        GetComponent<Canvas>().enabled = true;
    }

    void Update()
    {
        CurrentScoretext.text = ScoreManager.CurrentScore.ToString();
        HighScoretext.text = ScoreManager.HighScore.ToString();
    }
}
