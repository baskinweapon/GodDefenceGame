using System;
using System.Threading.Tasks;
using Cinemachine;
using GameFlow;
using UnityEngine;

public class CameraSystem: ICameraSystem {
    private Camera camera;
    private CinemachineVirtualCamera virtualCamera;
    
    public CameraSystem() {
        camera = Camera.main;
        if (camera != null) virtualCamera = camera.GetComponent<CinemachineVirtualCamera>();
    }
    
    public void ForceCameraPath() {
        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 3;
        UIManager.instance.tutor.gameObject.SetActive(false);
    }
    
    public async void TutorCameraPath() {
        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 1;
        UIManager.instance.tutor.gameObject.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(4));
        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 2;
        await Task.Delay(TimeSpan.FromSeconds(10));
        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 3;
        await Task.Delay(TimeSpan.FromSeconds(2));
        UIManager.instance.tutor.gameObject.SetActive(false);
    }
    
    public async void ShakeCamera(float duration, float magnitude) {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = magnitude;
        await Task.Delay(TimeSpan.FromSeconds(duration));
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
    
    public Camera GetCamera() {
        return camera;
    }
}
