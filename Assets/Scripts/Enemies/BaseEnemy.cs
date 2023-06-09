using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies {
    
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseEnemy : MonoBehaviour {
        public int health;
        public int damage;
        public int maxHealth;
        public float attackCooldown;
        public Slider slider;
        protected Animator animator;
        public int deathCost;
        
        protected bool death;
        public void Awake() {
            animator = GetComponent<Animator>();
            health = maxHealth;
        }

        public void OnEnable() {
            slider.gameObject.SetActive(true);
            death = false;
            health = maxHealth;
            slider.value = 1;
        }
        
        protected void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.CompareTag("Player")) {
                EnemySpawnAndMovement.OnDeleteEnemy(this.transform);
                animator.SetBool("Attack", true);
                StartCoroutine(Attack());
            }
        }
        
        private WaitForSeconds _wait;
        IEnumerator Attack() {
            _wait = new WaitForSeconds(attackCooldown);
            while (!death) {
                GameManager.instance.OnPlayerPassDamage?.Invoke(damage);
                yield return _wait;
            }
        }

        public virtual void Damage(int damage) {
            if (death) return;
            health -= damage;
            if (health <= 0) {
                health = 0;
                slider.value = 0;
                Death();
                return;
            }
            slider.value = (float)health / maxHealth;
            animator.SetTrigger("Damage");
        }

        protected virtual void Death() {
            EnemySpawnAndMovement.OnDeleteEnemy(this.transform);
            GameManager.instance.currencySystem.AddCurrency(deathCost);
            slider.gameObject.SetActive(false);
            animator.SetBool("Attack", false);
            animator.SetTrigger("Death");
            death = true;
            Invoke(nameof(Disable), 5f);
        }
        
        public void Disable() {
            GameManager.instance.saveSystem.GetGameSettings().data.enemyStatictics.sizeOfEnemyKilled++;
            gameObject.SetActive(false);
        }
    }
}