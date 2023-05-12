
using System;
using GameFlow;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : Singleton<GameMain> {
    private int _currency;
    
    public Action OnStartGame;
    public Action<int> OnChangeCurrency;

    public void AddCurrency(int value) {
        _currency += value;
        OnChangeCurrency?.Invoke(_currency);
    }

    public int GetCurrency() => _currency;
    
    public void Start() {
        StartNewGame();
        Pause();
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void Resume() {
        Time.timeScale = 1;
    }

    private void StartNewGame() {
        UIManager.instance.ShowScreen<StartGameCanvas>();
        OnStartGame?.Invoke();
    }
    
    public void LoadGameScene() {
        Resume();
        SceneManager.LoadScene(1);
        UIManager.instance.HideScreen<StartGameCanvas>();
    }
    
}
