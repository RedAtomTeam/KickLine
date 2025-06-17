using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTabsChanger : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _nextButton;

    [SerializeField] private GameObject _mainWindow;
    [SerializeField] private GameObject _levelsWindow;
    [SerializeField] private List<GameObject> _tabs;
    private int _currentTab = 0;


    private void OnEnable()
    {
        _currentTab = 0;
        OpenCurrentTab();
    }

    private void Awake()
    {
        _backButton.onClick.AddListener(() =>
        {
            if (_currentTab == 0)
            {
                _mainWindow.SetActive(true);
                _levelsWindow.SetActive(false);
            }
            else
            {
                _currentTab--;
                OpenCurrentTab();
            }
        });
        _nextButton.onClick.AddListener(() =>
        {
            if (_currentTab < _tabs.Count-1)
            {
                _currentTab++;
                OpenCurrentTab();
            }
        });
    }

    private void UpdateNextButton()
    {
        if (_currentTab == _tabs.Count - 1)
            _nextButton.gameObject.SetActive(false);
        else
            _nextButton.gameObject.SetActive(true);
    }

    private void OpenCurrentTab()
    {
        for (int i = 0; i < _tabs.Count; i++)
        {
            _tabs[i].SetActive(false);
        }
        _tabs[_currentTab].SetActive(true);
        UpdateNextButton();
    }






}
