using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreConfig", menuName = "Scriptable Objects/StoreConfig")]
public class StoreConfig : ScriptableObject
{
    public int money;
    public List<StoreStuff> stuff;

    public event Action _storeUpdateStatesEvent;
    public event Action _storeUpdateBalanceEvent;

    public void PerformUpdateStates()
    {
        _storeUpdateStatesEvent?.Invoke();
    }

    public void PerformUpdateBalance()
    {
        _storeUpdateBalanceEvent?.Invoke();
    }
}

[CreateAssetMenu(fileName = "StoreStuff", menuName = "Scriptable Objects/StoreStuff")]
public class StoreStuff : ScriptableObject
{
    public string stuffName;
    public bool isBuy;
    public int price;
    public bool isSelected;
    public List<StoreStuff> siblings;
} 