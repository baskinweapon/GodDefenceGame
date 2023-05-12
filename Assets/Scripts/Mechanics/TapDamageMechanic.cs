using UnityEngine;

public class TapDamageMechanic: MonoBehaviour {
    public ParticleSystem tapEffect;
    public float damagePower = 1f;
    
    private void OnEnable() {
        InputSystem.OnFire += OnFire;
    }
    
    private void OnFire() {
        var ray = Camera.main.ScreenPointToRay(InputSystem.instance.GetMousePosition());
        if (!Physics.Raycast(ray, out var hit)) return;
        if (!hit.collider.CompareTag("Enemy")) return;
        var enemy = hit.collider.GetComponent<Health>();
        if (enemy != null) {
            enemy.Damage(damagePower);
            PlayEffect(enemy.transform.position);
        }
    }

    private void PlayEffect(Vector3 point) {
        CameraManager.instance.ShakeCamera();
        var effect = Instantiate(tapEffect, transform);
        effect.transform.position = point;
        effect.Play();
    }

    private void OnDisable() {
        InputSystem.OnFire -= OnFire;
    }
}
