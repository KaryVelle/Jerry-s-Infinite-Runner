using System;
using Unity.VisualScripting;
using UnityEngine;

public class ItemRestorePos : MonoBehaviour
{
    public LayerMask layerMask;
    [SerializeField] private Transform startPosPlayer;
    [SerializeField] private GameObject player;
    [SerializeField] private int amountLeft;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((layerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            other.gameObject.transform.position = new Vector3(startPosPlayer.position.x, other.transform.position.y,
                startPosPlayer.position.z);
            gameObject.SetActive(false);
        }
    }

    public void RestorePos()
    {
        if (amountLeft >= 0)
        {
            player.transform.position = new Vector3(startPosPlayer.position.x, player.transform.position.y,
                startPosPlayer.position.z);
            amountLeft--;
        }
        else
        {
            Debug.Log("<color=yellow>No more items left.</color>");
        }
    }
}
