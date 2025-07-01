using UnityEngine;

public class SOLoader : MonoBehaviour
{
    static public SOLoader Instance;

    [SerializeField] private StoreConfig _storeConfig;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadAll();
            _storeConfig._storeUpdateBalanceEvent += SaveAll;
            _storeConfig._storeUpdateStatesEvent += SaveAll;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void SaveAll()
    {
        PlayerPrefs.SetInt("Balance", _storeConfig.money);
        print("UpdateBalance");
        
        foreach (var stuff in _storeConfig.stuff)
        {
            PlayerPrefs.SetInt($"{stuff.stuffName}_isBuy", stuff.isBuy ? 1 : 0);
            PlayerPrefs.SetInt($"{stuff.stuffName}_isSelected", stuff.isSelected ? 1 : 0);
        }
    }

    public void LoadAll()
    {
        _storeConfig.money = PlayerPrefs.GetInt("Balance", 0);

        foreach (var stuff in _storeConfig.stuff)
        {
            stuff.isBuy = PlayerPrefs.GetInt($"{stuff.stuffName}_isBuy", stuff.isBuy ? 1 : 0) == 1 ? true : false;
            stuff.isSelected = PlayerPrefs.GetInt($"{stuff.stuffName}_isSelected", stuff.isSelected ? 1 : 0) == 1 ? true : false;
        }
    }

}
