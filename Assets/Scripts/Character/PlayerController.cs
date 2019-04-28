using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerStats playerStats;

        Rigidbody2D body;
        Animator animator;

        bool bumping;
        bool running;

        float horizontal;
        float vertical;

        public AudioClip[] footsteps;
        public float footsteps_sound_delay_full;
        private float footsteps_sound_delay;
        private AudioSource footsteps_sound_player;

        void Start()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            footsteps_sound_player = GetComponent<AudioSource>();
        }

        void ToggleRunning()
        {
            running = !running;
        }

        public void StopRunning()
        {
            running = false;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ToggleRunning();
            }
            // Gives a value between -1 and 1
            horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            vertical = Input.GetAxisRaw("Vertical"); // -1 is down
            footsteps_sound_delay -= Time.deltaTime;
        }

        void FixedUpdate()
        {
            if (horizontal != 0 && vertical != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, so you move at 70% speed
                horizontal *= playerStats.moveLimiter;
                vertical *= playerStats.moveLimiter;
            }
            else if (running && horizontal == 0 && vertical == 0)
            {
                running = false;
            }

            float speed;
            if (running)
            {
                speed = playerStats.RunSpeed;
            }
            else
            {
                speed = playerStats.WalkSpeed;
            }

            if (bumping)
            {
                body.velocity = new Vector2(body.velocity.x / 1.5f, body.velocity.y / 1.5f);
            }
            else
            {
                body.velocity = new Vector2(horizontal * speed, vertical * speed);
            }

            if (body.velocity.magnitude > 0.0f)
            {
                animator.speed = body.velocity.magnitude;
                if (footsteps_sound_delay <= 0.0f)
                {
                    footsteps_sound_player.clip = footsteps[Mathf.RoundToInt(Random.Range(0, footsteps.Length))];
                    footsteps_sound_player.Play();
                    footsteps_sound_delay = footsteps_sound_delay_full / animator.speed;
                }
            }

            animator.SetFloat("speed", body.velocity.magnitude);
            animator.SetFloat("horizontal", horizontal);
            animator.SetFloat("vertical", vertical);
        }

        public void BumpPlayerIntoDirection(Vector2 direction)
        {
            bumping = true;
            body.velocity = direction;
            StartCoroutine("Bumping");
        }

        private IEnumerator Bumping()
        {
            yield return new WaitForSeconds(0.7f); // wait half a second
            bumping = false;
        }
    }
}