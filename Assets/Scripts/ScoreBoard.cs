using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    int score = 0;
    Text scoreText = null;

    private void Awake() 
    {
        scoreText = GetComponent<Text>();
    }

    private void Start() 
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString("### ##0");
    }

    public void ScoreHit(int scorePerHit)
    {
        score += scorePerHit;
        UpdateScore();
    }
}
