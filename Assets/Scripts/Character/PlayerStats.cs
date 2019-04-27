using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PlayerStats : MonoBehaviour
    {
        private const int STARTING_HEALTH = 255;
        private const float STARTING_PPS = 0.45f;

        public int CurrentHitPoints { get; private set; }
        public int MaxHitPoints { get; private set; }
        public float ProjectilesPerSecond { get; private set; }

        void Start()
        {
            this.MaxHitPoints = STARTING_HEALTH;
            this.CurrentHitPoints = STARTING_HEALTH;
            this.ProjectilesPerSecond = STARTING_PPS;
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
