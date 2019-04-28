using Assets.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class ProjectileBehavior : MonoBehaviour
    {
        public IWeaponProjectileEmitter shotFrom { get; set; }

        void OnCollisionEnter2D(Collision2D col)
        {
            for (var i = 0; i < col.contacts.Length; ++i)
            {
                var hit = col.GetContact(0).collider.gameObject;
                
                var damagable = hit.GetComponent<IDamagable>();
                damagable?.TakeDamage(shotFrom.GetDamage());

                this.gameObject.SetActive(false);
                return;
            }
        }
    }
}
