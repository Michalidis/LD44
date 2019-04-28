using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PlayerStats : MonoBehaviour
    {
        public readonly float moveLimiter = 0.7f;

        private const int STARTING_HEALTH = 255;
        private const float STARTING_PPS = 2.15f;
        private const float STARTING_WALK_SPEED = 1.0f;
        private const float STARTING_RUN_SPEED = 1.7f;

        public int CurrentHitPoints { get; private set; }
        public int MaxHitPoints { get; private set; }
        public float ProjectilesPerSecond { get; private set; }
        public float WalkSpeed { get; private set; }
        public float RunSpeed { get; private set; }

        void Start()
        {
            this.MaxHitPoints = STARTING_HEALTH;
            this.CurrentHitPoints = STARTING_HEALTH;
            this.ProjectilesPerSecond = STARTING_PPS;
            this.WalkSpeed = STARTING_WALK_SPEED;
            this.RunSpeed = STARTING_RUN_SPEED;
            GameObject.FindGameObjectWithTag("UI").GetComponent<UI.UIBehavior>().SetHealth(STARTING_HEALTH, STARTING_HEALTH);
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
    }
}
