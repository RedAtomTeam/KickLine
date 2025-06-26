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

    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _loseClip;

    [SerializeField] private GameObject _nextBtn;

    private bool _isPerform = false;


    private void Awake()
    {
        _borders.borderBallEvent += () => { PerformEndWindow("FAIL!", 0, _loseClip, false); };
        _gate.ballTriggerEvent += () => { PerformEndWindow("SUCCESS!", 100, _winClip, true); };
    }

    private void PerformEndWindow(string lableText, int moneyValue, AudioClip clip, bool isWin)
    {
        if (!_isPerform)
        {
            _body.SetActive(true);
            _isPerform = true;
            _labelText.text = lableText;
            _moneyText.text = moneyValue.ToString();
            _store.money += moneyValue;
            _nextBtn.SetActive(isWin);
            AudioService.Instance.PlayEffect(clip);
        }
    }
}
