using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
    [SerializeField][HideInInspector]
    private float curHealth = 100f;
    [SerializeField][HideInInspector]
    private float maxHealth = 100f;
    
    public UnityEvent<float> OnDamage;
    public UnityEvent OnDeath;
    
    public float GetCurrentHealth() => curHealth;
    public float GetCurrentHpPercent() => Mathf.InverseLerp(0, maxHealth, curHealth);
    public float GetMaxHealth() => maxHealth;
    
    public void Damage(float value) {
        if (curHealth == 0) return;
        curHealth -= value;
        if (curHealth <= 0) curHealth = 0;
        if (curHealth <= 0) {
            Death();
            return;
        }
        OnDamage?.Invoke(GetCurrentHpPercent());
    }
    
    public void Death() {
        GameMain.instance.AddCurrency(10);
        OnDeath?.Invoke();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Health))]
public class HealthEditor : Editor {
    public override void OnInspectorGUI() {
        var me = target as Health;
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Damage")) {
            if (me != null) me.Damage(10f);
        }
        
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Death")) {
            if (me != null) me.Death();
        }
    }
}

#endif
