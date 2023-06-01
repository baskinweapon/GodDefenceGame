public interface IGameFlowControl {
    public void Play();
    public void Pause();
    public void ChangeSpeed(float speed);
    public GameFlowManager GetGameFlowManager();
}
