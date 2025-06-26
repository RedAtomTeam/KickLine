using System;
using UnityEngine;

public class Border : MonoBehaviour
{
    public event Action ballTriggerEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ballTriggerEvent?.Invoke();
            collision.gameObject.SetActive(false);
        }    
    }

}
