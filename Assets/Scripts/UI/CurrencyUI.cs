using System;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour {
    public TextMeshProUGUI text;
    
    private void Awake() {
        GameManager.instance.currencySystem.OnCurrencyChange += ShowCurrency;
    }

    private void ShowCurrency(int value) {
        text.text = value.ToString();
    }

    private void OnDisable() {
        GameManager.instance.currencySystem.OnCurrencyChange -= ShowCurrency;
    }
}
