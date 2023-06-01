using System;
using UI;
using UnityEngine;

public class UpgradeUI : MonoBehaviour, IUIScreen {
    public Canvas canvas;
    
    public void Show() {
        canvas.sortingOrder = 100;
        canvas.gameObject.SetActive(true);
        GameManager.instance.gameFlowControl.Pause();
    }

    public void Hide() {
        canvas.sortingOrder = 0;
        canvas.gameObject.SetActive(false);
        GameManager.instance.gameFlowControl.Play();
    }
    
    public void OpenClose() {
        if (canvas.gameObject.activeSelf) {
            Hide();
        } else {
            Show();
        }
    }
}
