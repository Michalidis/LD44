using Assets.Interfaces;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Monsters
{
    public class EnemyStats : MonoBehaviour, IDamagable
    {
        [SerializeField] private int maxHitPoints;
        [SerializeField] private int damage;
        public int CurrentHitPoints { get; private set; }

        private float statMultiplier = 1f;

        void Start()
        {
            this.CurrentHitPoints = maxHitPoints;
        }

        public void TakeDamage(int amount)
        {
            this.CurrentHitPoints = (int) Mathf.Clamp(this.CurrentHitPoints - amount, 0f, this.maxHitPoints);

            this.TryUpdateHealthBar();


            if (this.CurrentHitPoints == 0)
            {
                JustDied();
            }
        }

        public void SetStatMultiplier(float multiplier)
        {
            this.statMultiplier = multiplier;

            this.CurrentHitPoints = (int)(this.statMultiplier * this.CurrentHitPoints);
            this.maxHitPoints = (int) (this.statMultiplier * this.maxHitPoints);

            this.TryUpdateHealthBar();
        }

        void TryUpdateHealthBar()
        {
            var healthBar = this.gameObject.GetComponent<HealthBarBehavior>();

            if (healthBar != null)
            {
                healthBar.SetHealth(this.CurrentHitPoints, this.maxHitPoints);
            }
        }

        void JustDied()
        {
            GameObject loot = GetComponent<LootManager>().GetLootItem();
            if (loot)
            {
                GameObject instance = Instantiate(loot);
                loot.transform.localPosition = transform.localPosition;
            }
            Destroy(gameObject);
        }

        public int GetDamage()
        {
            return (int)(this.damage * this.statMultiplier);
        }
    }
}
