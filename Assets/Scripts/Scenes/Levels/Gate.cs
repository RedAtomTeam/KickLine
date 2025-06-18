using System;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public event Action ballTriggerEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ballTriggerEvent?.Invoke();
        }
    }
}
