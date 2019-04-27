using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PlayerStats : MonoBehaviour
    {
        private const int STARTING_HEALTH = 255;

        public int CurrentHitPoints { get; private set; }
        public int MaxHitPoints { get; private set; }

        void Start()
        {
            this.MaxHitPoints = STARTING_HEALTH;
            this.CurrentHitPoints = STARTING_HEALTH;
            GameObject.FindGameObjectWithTag("UI").GetComponent<Assets.Scripts.UI.UIBehavior>().SetHealth(STARTING_HEALTH, STARTING_HEALTH);
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
