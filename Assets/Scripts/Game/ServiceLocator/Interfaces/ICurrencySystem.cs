using System;

public interface ICurrencySystem {
    public int GetCurrency();
    public void AddCurrency(int value);
    public bool SpendCurrency(int value);
    public CurrencySystem GetCurrencySystem();
}
