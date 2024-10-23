using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text highScoreText;
    public Text scoreText;
    int score;
    int highScore;

    // Start is called before the first frame update
    void Start()
    {
        SetHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        score = PlayerPrefs.GetInt("Score");
        scoreText.text = score.ToString();
    }

    public void SetHighScore()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
