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

        void Start()
        {
            this.CurrentHitPoints = maxHitPoints;
        }

        public void TakeDamage(int amount)
        {
            Debug.Log($"ENEMY TOOK DAMAGE: {amount}");
            this.CurrentHitPoints = (int) Mathf.Clamp(this.CurrentHitPoints - amount, 0f, this.maxHitPoints);

            var healthBar = this.gameObject.GetComponent<HealthBarBehavior>();
            Debug.Log(healthBar);
            if (healthBar != null)
            {
                healthBar.SetHealth(this.CurrentHitPoints, this.maxHitPoints);
            }


            if (this.CurrentHitPoints == 0)
            {
                JustDied();
            }
        }

        void JustDied()
        {
            GameObject loot = GetComponent<LootManager>().GetLootItem();
            if (loot)
            {
                GameObject instance = Instantiate(loot, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }

        public int GetDamage()
        {
            return this.damage;
        }
    }
}
