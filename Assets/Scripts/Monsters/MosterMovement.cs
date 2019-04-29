using UnityEngine;

namespace Assets.Scripts.Monsters
{
    public class MonsterMovement : MonoBehaviour
    {
        protected GameObject Player;

        protected Animator Animator;

        protected Rigidbody2D body;

        protected System.Random random = new System.Random();

        public float Speed;

        private float initialSpeed;

        protected void Spawn()
        {
            this.Pause();
            Invoke("UnPause", 1.5f);
        }

        public virtual void Pause()
        {
            this.initialSpeed = this.Speed;
            this.Speed = 0f;
            this.gameObject.GetComponent<Collider2D>().enabled = false;

            var weaponEmitter = this.GetComponentInChildren<WeaponProjectileEmitterMonster>();
            if (weaponEmitter != null)
            {
                weaponEmitter.Pause();
            }
        }

        public virtual void UnPause()
        {
            this.Speed = this.initialSpeed;
            this.gameObject.GetComponent<Collider2D>().enabled = true;

            var weaponEmitter = this.GetComponentInChildren<WeaponProjectileEmitterMonster>();
            if (weaponEmitter != null)
            {
                weaponEmitter.UnPause();
            }
        }

        protected virtual void Init()
        {
            if (this.Animator == null)
            {
                this.Animator = this.gameObject.GetComponent<Animator>();
            }

            if (this.Player == null)
            {
                this.Player = GameObject.Find("Character");
            }

            this.body = this.gameObject.GetComponent<Rigidbody2D>();
            this.body.rotation = 0;
            this.body.freezeRotation = true;
        }

        /*
     * 
     *   +Y
     * -X  +X   
     *   -Y
     * 
     * */
        protected void SetAnimator(Vector2 velocity)
        {
            if (velocity.magnitude < 0.001f)
            {
                Animator.SetBool("MoveUp", false);
                Animator.SetBool("MoveDown", false);
                Animator.SetBool("MoveRight", false);
                Animator.SetBool("MoveLeft", false);
            }
            else if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y)) // X Maior
            {
                if (velocity.x > 0)
                {
                    Animator.SetBool("MoveUp", false);
                    Animator.SetBool("MoveDown", false);
                    Animator.SetBool("MoveRight", true);
                    Animator.SetBool("MoveLeft", false);
                }
                else
                {
                    Animator.SetBool("MoveUp", false);
                    Animator.SetBool("MoveDown", false);
                    Animator.SetBool("MoveRight", false);
                    Animator.SetBool("MoveLeft", true);
                }
            }
            else // Y Maior
            {
                if (velocity.y > 0)
                {
                    Animator.SetBool("MoveUp", true);
                    Animator.SetBool("MoveDown", false);
                    Animator.SetBool("MoveRight", false);
                    Animator.SetBool("MoveLeft", false);
                }
                else
                {
                    Animator.SetBool("MoveUp", false);
                    Animator.SetBool("MoveDown", true);
                    Animator.SetBool("MoveRight", false);
                    Animator.SetBool("MoveLeft", false);
                }
            }
        }

        public void SetDeathAnimation()
        {
            Animator.SetBool("MoveUp", false);
            Animator.SetBool("MoveDown", false);
            Animator.SetBool("MoveRight", false);
            Animator.SetBool("MoveLeft", false);
            Animator.SetBool("Death", true);
        }
    }
}
