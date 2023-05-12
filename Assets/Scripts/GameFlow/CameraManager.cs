using System.Collections;
using Cinemachine;
using Enemies;
using GameFlow;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private CinemachineVirtualCamera _camera;
    
    private void Start() {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }
    
    public void TutorCameraPath() {
        StopAllCoroutines();
        StartCoroutine(CameraPathProccess());
    }

    private IEnumerator CameraPathProccess() {
        _camera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 1;
        UIManager.instance.tutor.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        _camera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 2;
        yield return new WaitForSeconds(10f);
        _camera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 3;
        yield return new WaitForSeconds(2f);
        UIManager.instance.tutor.gameObject.SetActive(false);
    }
    
    public void ShakeCamera() {
        StartCoroutine(ShakeCameraProccess());
    }
    
    private IEnumerator ShakeCameraProccess() {
        _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        yield return new WaitForSeconds(0.1f);
        _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}
