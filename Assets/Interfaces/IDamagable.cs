using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Interfaces
{
    interface IDamagable
    {
        void TakeDamage(int amount, bool apply_on_hit_effects = true);
    }

    public abstract class VisibleDamagableBehavior : MonoBehaviour, IDamagable
    {

        [SerializeField] private GameObject damageText;

        public virtual void TakeDamage(int amount, bool apply_on_hit_effects = true)
        {
            this.ShowDamageTaken(amount);
        }

        protected void ShowDamageTaken(int amount)
        {
            var dmgText = GameObject.Instantiate(damageText, this.transform);
            dmgText.GetComponentInChildren<DamageTaken>().SetDamage(amount);
        }

    }
}
