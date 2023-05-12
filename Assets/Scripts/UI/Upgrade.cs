using UnityEngine;

public class Upgrade : MonoBehaviour {

    public GameObject upgradeView;
    
    public void OpenClose() {
        upgradeView.SetActive(!upgradeView.activeSelf);
        if (upgradeView.activeSelf) {
            GameMain.instance.Pause();
        } else {
            GameMain.instance.Resume();
        }
    }
    
}
