using Assets.Scripts.Character;
using System;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject Player { get; set; }

    public float Speed { get; set; } = 0.3f;

    public Animator Animator;

    private Rigidbody2D col;

    // Start is called before the first frame update
    private void Start()
    {
        if (this.Player == null)
        {
            this.Player = GameObject.Find("Character");
        }

        this.col = this.gameObject.GetComponent<Rigidbody2D>();
        this.col.rotation = 0;
        this.col.freezeRotation = true;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 plPosition = this.Player.gameObject.transform.position;
        float x = plPosition.x;
        float y = plPosition.y;

        float thisX = this.gameObject.transform.position.x;
        float thisY = this.gameObject.transform.position.y;

        Vector2 dist = new Vector2(x - thisX, y - thisY).normalized;

        this.col.velocity = dist * Speed;

        SetAnimator(this.col.velocity);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player)
        {
            float x = collision.gameObject.transform.position.x;
            float y = collision.gameObject.transform.position.y;

            float thisX = this.gameObject.transform.position.x;
            float thisY = this.gameObject.transform.position.y;

            Vector2 dist = new Vector2(thisX - x, thisY - y).normalized;

            Player.GetComponent<PlayerController>().BumpPlayerIntoDirection(-dist * 10.0f);
        }
    }

    /*
     * 
     *   +Y
     * -X  +X   
     *   -Y
     * 
     * */
    private void SetAnimator(Vector2 velocity)
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
}
