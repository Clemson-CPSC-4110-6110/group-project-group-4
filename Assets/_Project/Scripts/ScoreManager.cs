using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public TextMeshPro scoreText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateScore();
    }

    public void AddPoint()
    {
        score += 1;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}