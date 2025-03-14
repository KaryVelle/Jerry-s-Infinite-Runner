using System;
using UnityEngine;

public class LooseCondition : MonoBehaviour
{
    public delegate void Death();
    public event Death OnDeath;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnDeath?.Invoke();
            Time.timeScale = 0;
        }
    }
}
