using System;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    [SerializeField] private AllPropsSpeed allPropsSpeed;
    [SerializeField] private Vector3 direction;


    private void Start()
    {
       allPropsSpeed = FindAnyObjectByType<AllPropsSpeed>();
    }

    private void Update()
    {
        transform.position += direction * (allPropsSpeed.currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Death"))
        {
            gameObject.SetActive(false);
        }
    }
}