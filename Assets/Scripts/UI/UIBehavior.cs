using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIBehavior : MonoBehaviour
    {
        [SerializeField] private HealthBarBehavior healthBar;

        public void SetHealth(int current, int max)
        {
            this.healthBar.SetHealth(current, max);
        }

    }
}
