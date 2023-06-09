using UnityEngine;

// Resoirces/SettingsAsset.asset - это ScriptableObject, который хранит в себе настройки игры.
public class SaveSystem: ISaveSystem {
    public SettingsAsset settingsAsset;

    public SaveSystem() {
        
        settingsAsset = Resources.Load<SettingsAsset>("SettingsAsset");
        if (!settingsAsset) {
            settingsAsset = ScriptableObject.CreateInstance<SettingsAsset>();
            #if Unity_Editor
            AssetDatabase.CreateAsset(settingsAsset, "Assets/Resources/"+ "SettingsAsset.asset");
            AssetDatabase.SaveAssets();
            #endif
        }
        settingsAsset.LoadFromFile();
    }
    
    public GameSettings GetGameSettings() {
        return settingsAsset.serializable;
    }

    public void Save() {
        settingsAsset.SaveToFile();
    }

    public void Load() {
        settingsAsset.LoadFromFile();
    }
    
    public void SetDefaultSave() {
        settingsAsset.SetDefaultSave();
    }
}
