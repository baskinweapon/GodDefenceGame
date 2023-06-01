public class SceneManager: ISceneManager {
    public void LoadScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneIndex) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
    
    public void UnloadScene(int scene) {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
    }
    
    public void UnloadScene(string scene) {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
    }
}

public enum Scenes {
    preloader,
    mainMenu,
    game
}
