using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private int score;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text timeText;

    [SerializeField]
    private float time = 60f;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private TMP_Text finalScoreText;

    private bool isGameOver;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        scoreText.text = "SCORE: " + score;
        timeText.text = "TIME: " + Mathf.CeilToInt(time);
    }

    void Update()
    {
        if (isGameOver)
            return;

        time -= Time.deltaTime;

        if (time <= 0)
        {
            time = 0;
            GameOver();
        }

        timeText.text = "TIME: " + Mathf.CeilToInt(time);
    }

    public void AddScore(int point)
    {
        score += point;

        scoreText.text = "SCORE: " + score;
    }

    private void GameOver()
    {
        isGameOver = true;

        finalScoreText.text = "FINAL SCORE: " + score;

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}