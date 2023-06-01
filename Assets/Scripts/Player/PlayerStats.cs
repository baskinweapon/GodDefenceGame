using GameFlow;

public class PlayerStats: IPlayerStats {
    public void ChangeMaxHealth(int amount) {
        GameManager.instance.saveSystem.GetGameSettings().data.player.maxHP += amount;
        UIManager.instance.PlayerHealthBar.maxValue = GameManager.instance.saveSystem.GetGameSettings().data.player.maxHP;
    }

    public void ChangeCurrentHealth(int amount) {
        GameManager.instance.saveSystem.GetGameSettings().data.player.ChangeCurrentHP(amount);
        UIManager.instance.PlayerHealthBar.maxValue = GameManager.instance.saveSystem.GetGameSettings().data.player.maxHP;
    }
}
