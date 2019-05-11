using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    public AudioClip oneUpSound;
    private AudioSource audioSource;
    private Text coinText;
    private int lastCoinValue;

    void Start()
    {
        coinText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        // Ef leikmaður nær í 10 peninga, spila 1-up hljóð
        if (lastCoinValue == 9)
        {
            audioSource.PlayOneShot(oneUpSound);
        }
        // Uppfæra texta
        lastCoinValue = VarManager.coins;
        string coinsString = lastCoinValue.ToString();
        coinsString = coinsString.PadLeft(2, '0');
        coinText.text = $"× {coinsString}";
    }

    void Update()
    {
        if (VarManager.coins != lastCoinValue)
        {
            UpdateCoinText();
        }
    }
}
