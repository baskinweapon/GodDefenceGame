public interface ISaveSystem {
    GameSettings GetGameSettings();
    void Save();
    void Load();
    void SetDefaultSave();
}
