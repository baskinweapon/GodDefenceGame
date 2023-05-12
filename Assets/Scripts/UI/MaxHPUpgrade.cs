using UnityEngine;

namespace UI {
    public class MaxHPUpgrade : UpgradePanel {
        
        public override void Upgrade() {
            if (cost > GameMain.instance.settings.serializable.playerSettings.currency) return;
            GameMain.instance.settings.serializable.playerSettings.maxHP += 10;
            GameMain.instance.AddCurrency(-cost);
            GameMain.instance.pyramidHealthBar.maxValue = GameMain.instance.settings.serializable.playerSettings.maxHP;
        }
    }
}