using System;
using UnityEngine;

public class GameFlowManager: IGameFlowControl {
    public Action OnPause;
    public Action OnPlay;
    public Action<float> OnChangeSpeed;

    private float timeScale = 1;
    public void Play() {
        Time.timeScale = timeScale;
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void ChangeSpeed(float speed) {
        timeScale = speed;
        Time.timeScale = speed;
    }

    public GameFlowManager GetGameFlowManager() {
        return this;
    }
}
