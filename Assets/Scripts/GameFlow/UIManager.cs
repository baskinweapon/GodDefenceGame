using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameFlow {
    public class UIManager : Singleton<UIManager> {

        public Canvas tutor;
        public Canvas GameOverCanvas;
        public Canvas UpgardeCanvas;
        
        public Slider PlayerHealthBar;
        
        public void ShowScreen<T>() where T : IUIScreen {
            IUIScreen screen = GetComponentInChildren<T>();
            screen.Show();
        }
        
        public void HideScreen<T>() where T : IUIScreen {
            IUIScreen screen = GetComponentInChildren<T>();
            screen.Hide();
        }
        
    }

   
}