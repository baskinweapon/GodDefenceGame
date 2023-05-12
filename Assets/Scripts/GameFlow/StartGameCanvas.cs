using UI;
using UnityEngine;

public class StartGameCanvas : MonoBehaviour, IUIScreen {
    public Canvas canvas;
    
    public void Show() {
        canvas.sortingOrder = 100;
        canvas.gameObject.SetActive(true);
    }

    public void Hide() {
        canvas.sortingOrder = 0;
        canvas.gameObject.SetActive(false);
    }
}
