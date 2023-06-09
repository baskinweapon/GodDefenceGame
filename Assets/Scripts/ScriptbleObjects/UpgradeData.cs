using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Game/Upgrade", order = 0)]
public class UpgradeData : ScriptableObject {
    public BaseUpgrade prefabUpgrade;
    public int cost;
    public Image image;
    public string named;
    public string description;

    private BaseUpgrade _upgrade;
    public BaseUpgrade GetUpgarde() {
        _upgrade = Instantiate(prefabUpgrade);
        return _upgrade;
    }
}
