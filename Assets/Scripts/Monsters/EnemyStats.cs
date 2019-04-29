using Assets.Interfaces;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Monsters
{
    public class EnemyStats : VisibleDamagableBehavior
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

        public override void TakeDamage(int amount, bool apply_on_hit_effects = true)
        {
            base.TakeDamage(amount, apply_on_hit_effects);

            this.CurrentHitPoints = (int) Mathf.Clamp(this.CurrentHitPoints - amount, 0f, this.maxHitPoints);

            this.TryUpdateHealthBar();


            if (this.CurrentHitPoints == 0)
            {
                JustDied();
                return;
            }
            else
            {
                if (apply_on_hit_effects)
                {
                    foreach (var item in p_itemManager.owned_items)
                    {
                        item.Value.OnEnemyStruck(player, gameObject);
                    }
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

            if (name != "Dragon")
            {
                this.gameObject.GetComponent<MonsterMovement>().Pause();
                Destroy(this.transform.GetChild(0).gameObject);
                this.gameObject.GetComponent<MonsterMovement>().SetDeathAnimation();
                Invoke("RemoveMe", 1.0f);
            }
            else
            {
                this.gameObject.GetComponent<MonsterMovement>().Pause();
                Destroy(this.transform.GetChild(0).gameObject);
                GetComponent<Animator>().SetBool("is_dead", true);
                Invoke("RemoveMe", 331.0f / 60.0f);
            }
        }

        void RemoveMe()
        {
            Destroy(gameObject);
        }

        public int GetDamage()
        {
            return (int)(this.damage * this.statMultiplier);
        }
    }
}
