using System;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth health;
        [SerializeField] private HeroMove move;
        [SerializeField] private HeroAnimator animator;
        [SerializeField] private GameObject deathFX;
        [SerializeField] private HeroAttack attack;
        
        private bool isDead;

        private void Start()
        {
            health.HealthChanged += HealthChanged;
        }

        private void OnDestroy() =>
            health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            Debug.Log("Health changed! : " +  health.Current);
            if (!isDead && health.Current <= 0)
                Die();
        }

        private void Die()
        {
            Debug.Log("HERO DIE!");
            isDead = true;
            attack.enabled = false;
            move.StopMoving();
            animator.PlayDeath();
            Instantiate(deathFX, transform.position, Quaternion.identity);
        }
    }
}