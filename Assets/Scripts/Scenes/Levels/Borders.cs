using System;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    [SerializeField] private List<Border> _borders;

    public event Action borderBallEvent;


    private void Awake()
    {
        foreach(var border in _borders)
            border.ballTriggerEvent += PerformBorderBallEvent;
    }

    private void PerformBorderBallEvent()
    {
        borderBallEvent?.Invoke();
    }

}
