using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    private Text coinText;

    void Start()
    {
        coinText = GetComponent<Text>();
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        string coinsString = VarManager.coins.ToString();
        coinsString = coinsString.PadLeft(2, '0');
        coinText.text = $"× {coinsString}";
    }

    void Update()
    {
        UpdateCoinText();
    }
}
