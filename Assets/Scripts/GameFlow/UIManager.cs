using UI;
using UnityEngine;

namespace GameFlow {
    public class UIManager : Singleton<UIManager> {
        
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