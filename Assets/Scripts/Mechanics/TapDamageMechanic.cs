using System.Collections;
using Enemies;
using UnityEngine;

/// <summary>
/// Main mechanic of the game. It allows to damage enemies by tapping on them.
/// </summary>
public class TapDamageMechanic: MonoBehaviour {
    public ParticleSystem tapEffect;
    public int damagePower = 1;
    public float cooldown = 0.5f;
    
    private void Start() {
        GameManager.instance.inputSystem.OnFire += OnFire;
    }
    
    private void OnFire() {
        StartCoroutine(HoldProcess());
    }
    
    private IEnumerator HoldProcess() {
        var inputSystem = GameManager.instance.inputSystem;
        
        // if mouse pressed we give more power to the damage
        while (inputSystem.IsPressed()) {
            var ray = GameManager.instance.cameraSystem.GetCamera().ScreenPointToRay(inputSystem.GetMousePosition());
            if (!Physics.Raycast(ray, out var hit)) yield break;
            if (!hit.collider.CompareTag("Enemy")) yield break;
            var enemy = hit.collider.GetComponent<BaseEnemy>();
            if (enemy != null) {
                enemy.Damage(damagePower);
                PlayEffect(enemy.transform.position);
            }
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void PlayEffect(Vector3 point) {
        GameManager.instance.cameraSystem.ShakeCamera(0.1f, 1);
        var effect = Instantiate(tapEffect, transform);
        effect.transform.position = point;
        effect.Play();
    }

    private void OnDisable() {
        GameManager.instance.inputSystem.OnFire -= OnFire;
    }
}
