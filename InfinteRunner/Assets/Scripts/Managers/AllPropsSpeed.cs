using Managers;
using UnityEngine;

public class AllPropsSpeed : MonoBehaviour
{
    [SerializeField] private GameDifficultyManager difficultySettings;
    public float currentSpeed;
    private int difficultyLevel = 0;

    private void OnEnable()
    {
        ScoreKeeper.OnScoreThresholdReached += IncreaseSpeed;
        currentSpeed = difficultySettings.baseSpeed;
    }

    private void OnDisable()
    {
        ScoreKeeper.OnScoreThresholdReached -= IncreaseSpeed;
    }

    private void IncreaseSpeed()
    {
        currentSpeed += difficultySettings.GetSpeedIncrement(difficultyLevel);
        difficultyLevel++;
        Debug.Log($"Nueva velocidad: {currentSpeed} (Nivel {difficultyLevel})");
    }
}