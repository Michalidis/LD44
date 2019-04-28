using Assets.Interfaces;
using Assets.Scripts.Character;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class Fountain : Interactable
    {
        [SerializeField] private GameObject bloodFountain;
        private float damage;

        void Start()
        {
            this.damage = (float)new System.Random().NextDouble() * 0.3f + 0.2f;
        }

        public override void PlayerInteract(GameObject player)
        {
            var playerStats = player.GetComponent<PlayerStats>();

            if (this.TryDamagePlayer(playerStats))
            {
                GameObject.FindGameObjectWithTag("UI").GetComponent<Assets.Scripts.UI.UIBehavior>().SetHealth(playerStats.CurrentHitPoints, playerStats.MaxHitPoints);
                GameObject.Instantiate(bloodFountain, this.transform.position, this.transform.rotation);
                GameObject.Destroy(this.gameObject);
            }
        }

        private bool TryDamagePlayer(PlayerStats playerStats)
        {   
            var damageTaken = playerStats.TryTakeDamagePercentage(this.damage);
            return damageTaken != 0;
        }
    }
}
