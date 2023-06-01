using System;
using GameFlow;
using UnityEditor;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    private ServiceLocator serviceLocator;
    public Action OnStartGame;
    public Action<int> OnPlayerPassDamage;

    protected override void Awake() {
        base.Awake();
        
        ServicesInit();
    }

    private void Start() {
        var currency = currencySystem.GetCurrencySystem();
        currency.OnCurrencyChange.Invoke(currencySystem.GetCurrency());
        StartNewGame();
        gameFlowControl.Pause();
    }

    #region Services
    
    public IInputSystem inputSystem;
    public ISaveSystem saveSystem;
    public ICameraSystem cameraSystem;
    public IGameFlowControl gameFlowControl;
    public ISceneManager sceneManager;
    public ICurrencySystem currencySystem;
    public IPlayerStats playerStats;
    
    private void ServicesInit() {
        serviceLocator = new ServiceLocator();
        inputSystem = serviceLocator.GetInputSystem();
        saveSystem = serviceLocator.GetSaveSystem();
        cameraSystem = serviceLocator.GetCameraSystem();
        gameFlowControl = serviceLocator.GetGameFlowControl();
        sceneManager = serviceLocator.GetSceneManager();
        currencySystem = serviceLocator.GetCurrencySystem();
        playerStats = serviceLocator.GetPlayerStats();
    }
    #endregion
    
    private void StartNewGame() {
        UIManager.instance.ShowScreen<StartGameCanvas>();
        OnStartGame?.Invoke();
    }
    
    public void LoadGameScene() {
        gameFlowControl.Play();
        UIManager.instance.GameOverCanvas.gameObject.SetActive(false);
        sceneManager.LoadScene(1);
        UIManager.instance.HideScreen<StartGameCanvas>();
        // cameraSystem.ForceCameraPath();
        cameraSystem.TutorCameraPath();
    }
    
    public void GameOver() {
        saveSystem.SetDefaultSave();
        gameFlowControl.Pause();
        sceneManager.UnloadScene(1);
        UIManager.instance.GameOverCanvas.gameObject.SetActive(true);
    }
    
    private void OnApplicationQuit() {
        saveSystem.Save();
    }

    private void OnDestroy() {
        inputSystem.GetInputSystem().OnDestroy();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Change speed game")) {
            GameManager.instance.gameFlowControl.ChangeSpeed(2);
        }
    }
}
#endif
