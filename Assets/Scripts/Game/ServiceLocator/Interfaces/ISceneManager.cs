public interface ISceneManager {
    public void LoadScene(string sceneName);
    public void LoadScene(int sceneIndex);
    public void UnloadScene(int scene);
    public void UnloadScene(string scene);
}
