using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score;
    [SerializeField] private Text scoreText;

    private void Start()
    {
        UpdateScoreText();
    }

    public void IncreaseScore(int score)
    {
        this.score += score;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
