using System;
using UnityEngine;

[Serializable]
public struct  GameSettings {
    public PlayerSettings playerSettings;
}

[Serializable]
public class PlayerSettings {
    public int currency;
    public int currentHP;
    public int maxHP;
}


