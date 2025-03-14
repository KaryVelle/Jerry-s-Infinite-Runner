using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public LooseCondition loose;

    [SerializeField] private Canvas looseCanvas;

    private void OnEnable()
    {
        loose.OnDeath += SetLoose;
    }

    private void OnDisable()
    {
        loose.OnDeath -= SetLoose;
    }

    private void SetLoose()
    {
        looseCanvas.enabled = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("ButonsTest");
        Time.timeScale = 1;
    }
}
