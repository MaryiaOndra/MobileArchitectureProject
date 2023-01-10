using System;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        [SerializeField] private HeroAnimator animator;

        private State state;
        private float max;
        private float current;

        public event Action HealthChanged;

        public float Current
        {
            get => current;
            set
            {
                current = value;
                if (state.currentHp != value)
                    HealthChanged?.Invoke();
                state.currentHp = current;
            }
        }

        public float Max
        {
            get => state.maxHp;
            set => state.maxHp = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            state = progress.heroState;
            Current = state.currentHp;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.heroState.currentHp = Current;
            progress.heroState.maxHp = Max;
        }

        public void TakeDamage(float damage)
        {
            Debug.Log("Take DAMEGE!: " + damage);
            if (Current <= 0)
                return;
            animator.PlayHit();
            Current -= damage;
            Debug.Log("HP: " + Current);
        }
    }
}