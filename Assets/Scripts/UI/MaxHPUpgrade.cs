
namespace UI {
    public class MaxHPUpgrade : UpgradePanel {
        
        public override void Upgrade() {
            if (GameManager.instance.currencySystem.SpendCurrency(cost)) {
                GameManager.instance.playerStats.ChangeMaxHealth(10);
            }
        }
    }
}