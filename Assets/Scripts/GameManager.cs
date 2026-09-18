using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private int score;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int point)
    {
        score += point;

        Debug.Log("Score: " + score);
    }
}