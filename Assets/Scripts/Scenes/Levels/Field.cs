using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    [SerializeField] private StoreConfig _storeConfig;
    [SerializeField] private Image _background;
    [SerializeField] private Image _label;

    [SerializeField] private StoreStuff _stuff;
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private Color _deselectedColor; 
    [SerializeField] private Color _selectedColor;

    [SerializeField] private Button _button;
    [SerializeField] private GameObject _selectWindow;
    [SerializeField] private GameObject _playWindow;


    private void Awake()
    {
        _storeConfig._storeUpdateStatesEvent += UpdateStatus;
        UpdateStatus();
    }


    public void UpdateStatus()
    {
        print("Updater state");
        if (_stuff.isBuy)
        {
            _label.gameObject.SetActive(true);
            if (_stuff.isSelected)
            {
                _label.color = _selectedColor;
                _text.gameObject.SetActive(true);
                _background.sprite = _stuff._sprite;
            }
            else
            {
                _label.color = _deselectedColor;
                _text.gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


    public void Select()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(SetField);

        foreach (var sib in _stuff.siblings)
            sib.isSelected = false;
        _stuff.isSelected = true;

        _storeConfig.PerformUpdateStates();

    }

    public void SetField()
    {
        _selectWindow.SetActive(false);
        _playWindow.SetActive(true);
    }

}
