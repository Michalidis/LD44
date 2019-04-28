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
                var hitedObject = col.GetContact(0).collider.gameObject;
                
                var damagable = hitedObject.GetComponent<IDamagable>();
                damagable?.TakeDamage(shotFrom.GetDamage());

                this.gameObject.SetActive(false);
                return;
            }
        }
    }
}
