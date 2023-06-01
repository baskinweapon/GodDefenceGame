using System;
using GameFlow;
using UnityEngine;
using UnityEngine.UI;

namespace Player {
    public class Player : MonoBehaviour {
        public Slider HealthBar;
        

        private void OnEnable() {
            GameManager.instance.OnStartGame += OnStartGame;
            GameManager.instance.OnPlayerPassDamage += PassDamage;
            HealthBar = UIManager.instance.PlayerHealthBar;
            HealthBar.maxValue = GameManager.instance.saveSystem.GetGameSettings().data.player.maxHP;
            HealthBar.value = GameManager.instance.saveSystem.GetGameSettings().data.player.currentHP;
        }
        
        private void OnStartGame() {
            HealthBar.maxValue = GameManager.instance.saveSystem.GetGameSettings().data.player.maxHP;
            HealthBar.value = GameManager.instance.saveSystem.GetGameSettings().data.player.currentHP;
        }
        
        public void PassDamage(int damage) {
            GameManager.instance.saveSystem.GetGameSettings().data.player.ChangeCurrentHP(-damage);
            HealthBar.value = GameManager.instance.saveSystem.GetGameSettings().data.player.currentHP;
            if (GameManager.instance.saveSystem.GetGameSettings().data.player.currentHP <= 0) {
                GameManager.instance.GameOver();
            }
        }
        
        private void OnDisable() {
            GameManager.instance.OnStartGame -= OnStartGame;
            GameManager.instance.OnPlayerPassDamage -= PassDamage;
        }
    }
}