using System;

public class CurrencySystem: ICurrencySystem {
    private int currency;
    public Action<int> OnCurrencyChange { get; set; }
    
    public CurrencySystem() {
        currency = GameManager.instance.saveSystem.GetGameSettings().data.player.currency;
    }
    public int GetCurrency() {
        return currency;
    }

    public void AddCurrency(int value) {
        currency += value;
        GameManager.instance.saveSystem.GetGameSettings().data.player.currency = currency;
        OnCurrencyChange?.Invoke(currency);
    }
    
    private bool AgreeTransaction(int value) {
        return currency >= value;
    }

    public bool SpendCurrency(int value) {
        if (AgreeTransaction(value)) {
            currency -= value;
            OnCurrencyChange?.Invoke(currency);
            GameManager.instance.saveSystem.GetGameSettings().data.player.currency = currency;
            return true;
        }

        return false;
    }

    public CurrencySystem GetCurrencySystem() {
        return this;
    }
}
