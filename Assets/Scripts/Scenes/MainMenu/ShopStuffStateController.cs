using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopStuffStateController : MonoBehaviour
{
    [SerializeField] private StoreConfig _storeConfig;
    [SerializeField] private StoreStuff _storeStuff;

    [SerializeField] private GameObject _isNotBuy;
    [SerializeField] private GameObject _isSelected;
    [SerializeField] private GameObject _isNotSelected;

    [SerializeField] private List<ShopStuffStateController> _controllers;

    [SerializeField] private TextMeshProUGUI _text;


    private void Awake()
    {
        _storeConfig._storeUpdateStatesEvent += UpdateState;   
    }

    private void OnEnable()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (_storeStuff != null)
        {
            if (_storeStuff.isSelected)
            {
                _isNotBuy.SetActive(false);
                _isSelected.SetActive(true);
                _isNotSelected.SetActive(false);
                _text.text = "IsSelected";
            }
            else
            {
                if (_storeStuff.isBuy)
                {
                    _isNotBuy.SetActive(false);
                    _isSelected.SetActive(false);
                    _isNotSelected.SetActive(true);
                    _text.text = "IsBuy";
                }
                else
                {
                    _isNotBuy.SetActive(true);
                    _isSelected.SetActive(false);
                    _isNotSelected.SetActive(false);
                    _text.text = "IsNotSelected";
                }
            }
        }
        
    }

    public void Buy() 
    {
        if (!_storeStuff.isBuy)
        {
            if (_storeConfig.money >= _storeStuff.price)
            {
                _storeConfig.money -= _storeStuff.price;
                _storeConfig.PerformUpdateBalance();
                _storeStuff.isBuy = true;
                //_storeConfig.PerformUpdateStates();
                UpdateState();

                foreach (var controller in _controllers) 
                    controller.UpdateState();
                _text.text = "Buy";
            }
        }
    }

    public void Select()
    {
        if (!_storeStuff.isSelected)
        {
            _storeStuff.isSelected = true;
            foreach (var sib in _storeStuff.siblings)
                sib.isSelected = false;
            UpdateState();            
        }
        //_storeConfig.PerformUpdateStates();
        foreach (var controller in _controllers)
            controller.UpdateState();
        _text.text = "Select";
    }
}
