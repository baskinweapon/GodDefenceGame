using UnityEngine;

public interface ICameraSystem {
    public Camera GetCamera();
    public void ShakeCamera(float duration, float magnitude);
    public void TutorCameraPath();
    public void ForceCameraPath();
}
