using TMPro;
using UnityEngine;

public class EndWindowPerformer : MonoBehaviour
{
    [SerializeField] private StoreConfig _store;
    [SerializeField] private Borders _borders;
    [SerializeField] private Gate _gate;

    [SerializeField] private GameObject _body;
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private TextMeshProUGUI _moneyText;

    private bool _isPerform = false;


    private void Awake()
    {
        _borders.borderBallEvent += () => { PerformEndWindow("FAIL!", 0); };
        _gate.ballTriggerEvent += () => { PerformEndWindow("SUCCESS!", 100); };
    }

    private void PerformEndWindow(string lableText, int moneyValue)
    {
        if (!_isPerform)
        {
            _body.SetActive(true);
            _isPerform = true;
            _labelText.text = lableText;
            _moneyText.text = moneyValue.ToString();
            _store.money += moneyValue;
        }
    }
}
