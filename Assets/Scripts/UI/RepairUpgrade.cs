using UnityEngine;

namespace UI {
    public class RepairUpgrade : UpgradePanel {
        
        public override void Upgrade() {
            if (cost > GameMain.instance.settings.serializable.playerSettings.currency) return;
            GameMain.instance.settings.serializable.playerSettings.currentHP = GameMain.instance.settings.serializable.playerSettings.maxHP;
            GameMain.instance.AddCurrency(-cost);
            GameMain.instance.pyramidHealthBar.value = GameMain.instance.settings.serializable.playerSettings.currentHP;
        }
    }
}