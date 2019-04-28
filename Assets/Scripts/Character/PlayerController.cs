using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PlayerController : MonoBehaviour
    {

        Rigidbody2D body;
        Animator animator;

        bool bumping;

        float horizontal;
        float vertical;
        float moveLimiter = 0.7f;

        public float runSpeed = 1.0f;

        void Start()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            // Gives a value between -1 and 1
            horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        }

        void FixedUpdate()
        {
            if (horizontal != 0 && vertical != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, so you move at 70% speed
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }

            if (bumping)
            {
                bumping = false;
            }
            else
            {
                body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
            }
            animator.SetFloat("speed", body.velocity.magnitude);
            animator.SetFloat("horizontal", horizontal);
            animator.SetFloat("vertical", vertical);
        }

        public void BumpPlayerIntoDirection(Vector2 direction)
        {
            bumping = true;
            body.velocity = direction;
        }
    }
}