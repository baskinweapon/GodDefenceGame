using System;

public interface ICurrencySystem {
    public Action<int> OnCurrencyChange { get; set; }
    public int GetCurrency();
    public void AddCurrency(int value);
    public bool SpendCurrency(int value);
}
