using Assets.Interfaces;
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
            if (this.CurrentHitPoints == 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        public int GetDamage()
        {
            return this.damage;
        }
    }
}
