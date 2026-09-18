using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private int score;

    [SerializeField]
    private TMP_Text scoreText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText.text = "SCORE: " + score;
    }

    public void AddScore(int point)
    {
        score += point;

        scoreText.text = "SCORE: " + score;
    }
}