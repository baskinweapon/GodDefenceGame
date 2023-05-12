using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour {
   public TextMeshProUGUI text;
   public Button _button;
   public int cost;
   public string name;

   public void OnEnable() {
      _button.onClick.AddListener(Upgrade);
      _button.GetComponentInChildren<TextMeshProUGUI>().text = cost.ToString();
      text.text = name;
   }

   public virtual void Upgrade() {
      
   }
   
   public void OnDisable() {
      _button.onClick.RemoveListener(Upgrade);
   }
}
