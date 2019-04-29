using Assets.Interfaces;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Monsters
{
    public class EnemyStats : MonoBehaviour, IDamagable
    {
        [SerializeField] private int maxHitPoints;
        [SerializeField] private int damage;
        private ItemManager p_itemManager;
        private GameObject player;
        public int CurrentHitPoints { get; private set; }

        private float statMultiplier = 1f;

        void Start()
        {
            this.CurrentHitPoints = maxHitPoints;
            p_itemManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemManager>();
            player = p_itemManager.gameObject;
        }

        public void TakeDamage(int amount)
        {
            this.CurrentHitPoints = (int) Mathf.Clamp(this.CurrentHitPoints - amount, 0f, this.maxHitPoints);

            this.TryUpdateHealthBar();


            if (this.CurrentHitPoints == 0)
            {
                JustDied();
                return;
            }
            else
            {
                foreach (var item in p_itemManager.owned_items)
                {
                    item.Value.OnEnemyStruck(player, gameObject);
                }
            }
        }

        public void SetStatMultiplier(float multiplier)
        {
            this.statMultiplier = multiplier;

            this.CurrentHitPoints = (int)(this.statMultiplier * this.CurrentHitPoints);
            this.maxHitPoints = (int) (this.statMultiplier * this.maxHitPoints);
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
                GameObject instance = Instantiate(loot, transform.position, transform.rotation);
            }
            foreach (var item in p_itemManager.owned_items)
            {
                item.Value.OnEnemyKilled(player, gameObject);
            }
            Destroy(gameObject);
        }

        public int GetDamage()
        {
            return (int)(this.damage * this.statMultiplier);
        }
    }
}
