using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static Action<ShapeType> OnScoreUpdateAction;
    public static Action OnScoreResetAction;

    public int squareScore = 10;
    public int circleScore = 15;
    public int triangleScore = 30;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;

    private int score = 0;

    private void Awake()
    {
        OnScoreUpdateAction += UpdateScore;
        OnScoreResetAction += ResetScore;
    }

    private void OnDestroy()
    {

        OnScoreUpdateAction -= UpdateScore;
        OnScoreResetAction -= ResetScore;
    }

    private void Start()
    {
        ResetScore();
    }

    private void ResetScore()
    {
        score = 0;
        scoreText.text = "SCORE: " + score.ToString();
        finalScoreText.text = "SCORE: " + score.ToString();
    }

    private void UpdateScore(ShapeType shapeType)
    {
        switch (shapeType)
        {
            case ShapeType.SQUARE:
                score += 10;
                break;
            case ShapeType.CIRCLE:
                score += 15;
                break;
            case ShapeType.TRIANGLE:
                score += 30;
                break;
            default:
                break;
        }

        scoreText.text = "SCORE: " + score.ToString();
        finalScoreText.text = "SCORE: " + score.ToString();
    }
}
