using Unity.VisualScripting;

public class ServiceLocator {
    private IInputSystem inputSystem;
    private ISaveSystem saveSystem;
    private ICameraSystem cameraSystem;
    private IGameFlowControl gameFlowControl;
    private ISceneManager sceneManager;
    private ICurrencySystem currencySystem;
    private IPlayerStats playerStats;
    
    public IInputSystem GetInputSystem() {
        return inputSystem ??= new InputSystem();
    }
    
    public ISaveSystem GetSaveSystem() {
        return saveSystem ??= new SaveSystem();
    }
    
    public ICameraSystem GetCameraSystem() {
        return cameraSystem ??= new CameraSystem();
    }
    
    public IGameFlowControl GetGameFlowControl() {
        return gameFlowControl ??= new GameFlowManager();
    }
    
    public ISceneManager GetSceneManager() {
        return sceneManager ??= new SceneManager();
    }
    
    public ICurrencySystem GetCurrencySystem() {
        return currencySystem ??= new CurrencySystem();
    }
    
    public IPlayerStats GetPlayerStats() {
        return playerStats ??= new PlayerStats();
    }
}
