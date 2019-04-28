using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterMovement : MonoBehaviour
{
    protected GameObject Player;

    protected Animator Animator;

    protected Rigidbody2D col;

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

        this.col = this.gameObject.GetComponent<Rigidbody2D>();
        this.col.rotation = 0;
        this.col.freezeRotation = true;
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
}
