using GameFlow;
using UnityEngine;

namespace UI {
    public class RepairUpgrade : UpgradePanel {
        
        public override void Upgrade() {
            if (GameManager.instance.currencySystem.SpendCurrency(cost)) {
                GameManager.instance.playerStats.ChangeCurrentHealth(GameManager.instance.saveSystem.GetGameSettings().data.player.maxHP - GameManager.instance.saveSystem.GetGameSettings().data.player.currentHP);
            }
        }
    }
}