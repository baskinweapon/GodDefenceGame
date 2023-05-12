using System.Collections;
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

        private bool death;
        protected virtual void OnDeath() {
            ZombieSpawner.OnDeleteEnemy(this.transform);
            animator.SetBool("Attack", false);
            animator.SetTrigger("Death");
            death = true;
            Invoke(nameof(Disable), 2f);
        }

        public void Disable() {
            gameObject.SetActive(false);
        }
        
        protected virtual void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.CompareTag("Pyramid")) {
                ZombieSpawner.OnDeleteEnemy(this.transform);
                animator.SetBool("Attack", true);
                StartCoroutine(AttackDammage());
            }
        }

        private WaitForSeconds _wait = new WaitForSeconds(1);
        IEnumerator AttackDammage() {
            while (!death) {
                GameMain.instance.PassPyramidDamage(1);
                yield return _wait;
            }
        }

        private void OnDisable() {
            _health.OnDamage.RemoveListener(OnDamage);
            _health.OnDeath.RemoveListener(OnDeath);
        }
    }
}