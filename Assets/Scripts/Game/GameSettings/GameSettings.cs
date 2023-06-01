using System;

[Serializable]
public struct  GameSettings {
    public Data data;
}

[Serializable]
public class Data {
    public CameraSettings camera;
    public PlayerData player;
    public EnemyStatictics enemyStatictics;
}

[Serializable]
public struct PlayerData{
    public int maxHP;
    public int currentHP;
    public int currency;
    
    public void ChangeCurrentHP(int value) {
        switch (value) {
            case < 0: {
                value *= -1;
                if (currentHP <= value) {
                    currentHP = 0;
                    return;
                }
                currentHP -= value;
                return;
            }
            case > 0 when (currentHP + value > maxHP):
                currentHP = maxHP;
                break;
        }
    }
}

[Serializable]
public struct EnemyStatictics {
    public int sizeOfEnemyKilled;
}

[Serializable]
public struct CameraSettings {
    public float cameraSpeed;
}


