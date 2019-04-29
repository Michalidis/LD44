using Assets.Interfaces;
using Assets.Scripts.Mechanics;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PlayerStats : MonoBehaviour, IDamagable
    {
        public WeaponProjectileEmitter emitter;
        public readonly float moveLimiter = 0.7f;

        private const int STARTING_HEALTH = 255;
        private const float STARTING_PPS = 2.15f;
        private const float STARTING_WALK_SPEED = 1.0f;
        private const float STARTING_RUN_SPEED = STARTING_WALK_SPEED * 1.7f;
        private const float STARTING_PROJECTILE_SPEED = 175.0f;
        private const int STARTING_PROJECTILE_DAMAGE = 9;

        public int CurrentHitPoints { get; private set; }
        public int MaxHitPoints { get; private set; }
        public float ProjectilesPerSecond { get; private set; }
        public float WalkSpeed { get; private set; }
        public float RunSpeed { get; private set; }


        public int Bonus_MaxHP;
        public float Bonus_AttackSpeedPct;
        public float Bonus_WalkSpeed;
        public float Bonus_RunSpeed;
        public float Bonus_ProjectileSpeed;
        public float Bonus_ProjectileDamagePct;

        public bool HasStoneKey;
        public bool HasGoldenKey;

        void Start()
        {
            HasStoneKey = false;
            HasGoldenKey = false;
            RecalculateBaseStats();
            this.CurrentHitPoints = this.MaxHitPoints;
            GameObject.FindGameObjectWithTag("UI").GetComponent<UI.UIBehavior>().SetHealth(STARTING_HEALTH, STARTING_HEALTH);
        }


        public void SetCurrentHealth(int amount)
        {
            CurrentHitPoints = amount;
        }

        public void RecalculateBaseStats()
        {
            this.MaxHitPoints = STARTING_HEALTH + Bonus_MaxHP;
            this.CurrentHitPoints = Mathf.Min(MaxHitPoints, CurrentHitPoints);
            this.ProjectilesPerSecond = STARTING_PPS * (1.0f + Bonus_AttackSpeedPct);
            this.WalkSpeed = STARTING_WALK_SPEED * (1.0f + Bonus_WalkSpeed);
            this.RunSpeed = STARTING_RUN_SPEED * (1.0f + Bonus_RunSpeed);
            this.emitter.shooting_force = STARTING_PROJECTILE_SPEED * (1.0f + Bonus_ProjectileSpeed);
            this.emitter.shooting_damage = (int)(STARTING_PROJECTILE_DAMAGE * (1.0f + Bonus_ProjectileDamagePct));
        }

        public int TryTakeDamagePercentage(float percentage)
        {
            int lifeToTake = (int) (this.MaxHitPoints * percentage);
            if (lifeToTake >= CurrentHitPoints)
            {
                return 0;
            }

            this.CurrentHitPoints -= lifeToTake;
            return lifeToTake;
        }

        public void TakeDamage(int amount)
        {
            this.CurrentHitPoints = (int) Mathf.Clamp(this.CurrentHitPoints - amount, 0.0f, this.MaxHitPoints);

            var ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI.UIBehavior>();
            ui.SetHealth(this.CurrentHitPoints, this.MaxHitPoints);

            if (this.CurrentHitPoints == 0)
            {
                if (GetComponent<ItemManager>().TryResurrect())
                {
                    CurrentHitPoints = MaxHitPoints;
                    return;
                }

                ui.OnPlayerDeath();

                this.gameObject.GetComponent<PlayerController>()?.Disable();
                this.gameObject.GetComponent<ObjectSpawner>()?.Disable();
            }

        }
    }
}
