using UnityEngine;
using UnityEngine.UI;

namespace Enemies {
    
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Zombie : MonoBehaviour {
        private Animator animator;
        private Health _health;
        public Slider slider;

        public void Awake() {
            animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
        }

        public void OnEnable() {
            _health.OnDamage.AddListener(OnDamage);
            _health.OnDeath.AddListener(OnDeath);
            slider.value = 1;
        }

        protected virtual void OnDamage(float damage) {
            animator.SetTrigger("Damage");
        }
        
        protected virtual void OnDeath() {
            ZombieSpawner.instance.DeleteEnemy(this.transform);
            animator.SetBool("Attack", false);
            animator.SetTrigger("Death");
            Invoke(nameof(Disable), 2f);
        }

        public void Disable() {
            gameObject.SetActive(false);
        }
        protected virtual void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.CompareTag("Pyramid")) {
                ZombieSpawner.instance.DeleteEnemy(this.transform);
                animator.SetBool("Attack", true);
            }
        }

        private void OnDisable() {
            _health.OnDamage.RemoveListener(OnDamage);
            _health.OnDeath.RemoveListener(OnDeath);
        }
    }
}