using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour {
    public TextMeshProUGUI text;
    
    private void Awake() {
        GameMain.instance.OnChangeCurrency += ShowCurrency;
    }

    private void ShowCurrency(int value) {
        text.text = value.ToString();
    }

    private void OnDisable() {
        GameMain.instance.OnChangeCurrency -= ShowCurrency;
    }
}
