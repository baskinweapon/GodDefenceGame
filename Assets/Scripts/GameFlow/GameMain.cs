
using System;
using GameFlow;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMain : Singleton<GameMain> {
    public SettingsAsset settings;
    
    public Action OnStartGame;
    public Action<int> OnChangeCurrency;
    
    public Slider pyramidHealthBar;

    public void AddCurrency(int value) {
        settings.serializable.playerSettings.currency += value;
        OnChangeCurrency?.Invoke(settings.serializable.playerSettings.currency);
    }

    public int GetCurrency() => settings.serializable.playerSettings.currency;

    private void OnEnable() {
        settings.LoadFromFile();
    }

    public void Start() {
        OnChangeCurrency?.Invoke(settings.serializable.playerSettings.currency);
        pyramidHealthBar.maxValue = settings.serializable.playerSettings.maxHP;
        pyramidHealthBar.value = settings.serializable.playerSettings.currentHP;
        StartNewGame();
        Pause();
    }

    public void PassPyramidDamage(int damage) {
        settings.serializable.playerSettings.currentHP -= damage;
        pyramidHealthBar.value = settings.serializable.playerSettings.currentHP;
        if (settings.serializable.playerSettings.currentHP <= 0) {
            GameOver();
        }
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

    private void GameOver() {
        settings.SetDefaultSave();
        Pause();
        SceneManager.UnloadSceneAsync(1);
        UIManager.instance.GameOverCanvas.gameObject.SetActive(true);
    }
    
    public void LoadGameScene() {
        Resume();
        UIManager.instance.GameOverCanvas.gameObject.SetActive(false);
        SceneManager.LoadScene(1);
        UIManager.instance.HideScreen<StartGameCanvas>();
        CameraManager.instance.TutorCameraPath();
    }

    private void OnApplicationQuit() {
        settings.SaveToFile();
    }
}
