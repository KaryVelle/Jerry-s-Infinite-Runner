using System;
using Managers;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private GameDifficultyManager difficultySettings; 
    [SerializeField] private float scoreMultiplier = 1.0f;
    private float score;
    private float gameTime;
    private bool isScoreCounting = true; // 🛠 Inicializamos en TRUE
    private float nextThreshold;

    public static event Action OnScoreThresholdReached;

    private void Start()
    {
        score = 0;
        gameTime = 0;
        nextThreshold = difficultySettings.GetNextThreshold(0);
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            isScoreCounting = false;
        }
        else
        {
            isScoreCounting = true; 
        }

        if (!isScoreCounting) return;

        gameTime += Time.deltaTime;
        score = scoreMultiplier * gameTime;
        tmp.text = "Score: " + Math.Ceiling(score);

        if (score >= nextThreshold)
        {
            OnScoreThresholdReached?.Invoke();
            nextThreshold = difficultySettings.GetNextThreshold(score);
            Debug.Log($"Nuevo umbral alcanzado: {nextThreshold}");
        }
    }
}