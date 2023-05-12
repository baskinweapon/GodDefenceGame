using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private CinemachineVirtualCamera _camera;
    
    private void Start() {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }
    
    public void ShakeCamera() {
        StopAllCoroutines();
        StartCoroutine(ShakeCameraProccess());
    }
    
    private IEnumerator ShakeCameraProccess() {
        _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        yield return new WaitForSeconds(0.1f);
        _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}
