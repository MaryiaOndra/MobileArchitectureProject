using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar HpBar;

        private IHealth health;

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();
            if(health != null)
                Construct(health);
        }

        private void OnDestroy()
        {
            if(health != null)
                health.HealthChanged -= UpdateHpBar;
        }

        public void Construct(IHealth health)
        {
            this.health = health;
            health.HealthChanged += UpdateHpBar;
        }

        private void UpdateHpBar() =>
            HpBar.SetValue(health.Current, health.Max);
    }
}