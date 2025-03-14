using UnityEngine;

namespace Managers
{
    [System.Serializable]
    public class GameDifficultyManager : MonoBehaviour
    {
        [Header("Score Thresholds")]
        [Tooltip("Lista de umbrales de puntuación para cambiar la dificultad.")]
        public float[] scoreThresholds = { 150, 300, 800, 1300, 2000 };

        [Tooltip("Incremento de puntuación después del umbral final.")]
        public float postMaxThresholdIncrement = 1000f;

        [Header("Speed Settings")]
        [Tooltip("Velocidad base de los objetos.")]
        public float baseSpeed = 1.0f;

        [Tooltip("Incrementos de velocidad por nivel.")]
        public float[] speedIncrements = { 0.5f, 0.7f, 1.0f, 1.2f, 1.5f };

        public float GetNextThreshold(float currentScore)
        {
            foreach (var threshold in scoreThresholds)
            {
                if (currentScore < threshold) return threshold;
            }
            return Mathf.Floor(currentScore / postMaxThresholdIncrement) * postMaxThresholdIncrement + postMaxThresholdIncrement;
        }

        public float GetSpeedIncrement(int level)
        {
            if (level < speedIncrements.Length)
                return speedIncrements[level];
            return speedIncrements[speedIncrements.Length - 1]; // Mantiene el último incremento
        }
    }
}