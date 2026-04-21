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

    public void AddPoints(int amount)
    {
        score += amount;
        UpdateScore();
    }

    public bool SpendPoints(int amount)
    {
        if (score >= amount)
        {
            score -= amount;
            UpdateScore();
            return true;
        }

        return false;
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}