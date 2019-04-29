using Assets.Scripts.Character;
using Assets.Scripts.Monsters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonsterMovement
{
    public AudioClip[] growls;
    private float soundEstimate;

    private PlayerController playerController;

    private Vector2 swappingVector;
    private AudioSource soundPlayer;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Speed = 0.35f;
        swappingVector = new Vector2(-1, 1);
        soundPlayer = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        Init();
        Spawn();

        playerController = Player.GetComponent<PlayerController>();

        soundPlayer.clip = growls[random.Next(growls.Length)];
    }

    void PlayGrowl()
    {
        if (soundEstimate < 0)
        {
            soundPlayer.clip = growls[random.Next(growls.Length)];
            soundEstimate = soundPlayer.clip.length + 5 + random.Next(5);

            soundPlayer.Play();
        }
        soundEstimate -= Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        this.PlayGrowl();

        Vector3 plPosition = Player.gameObject.transform.position;
        float x = plPosition.x;
        float y = plPosition.y;

        float thisX = gameObject.transform.position.x;
        float thisY = gameObject.transform.position.y;

        Vector2 dist = new Vector2(x - thisX, y - thisY).normalized;
        if (!animator.GetBool("is_dead"))
        {
            if (dist.x > 0 && transform.localScale.x > 0)
            {
                transform.localScale *= swappingVector;
            }
            else if (dist.x < 0 && transform.localScale.x < 0)
            {
                transform.localScale *= swappingVector;
            }
        }

        body.velocity = dist * Speed;
        animator.SetFloat("speed", body.velocity.magnitude);

        SetAnimator(body.velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player)
        {
            float x = collision.gameObject.transform.position.x;
            float y = collision.gameObject.transform.position.y;

            float thisX = gameObject.transform.position.x;
            float thisY = gameObject.transform.position.y;

            Vector2 dist = new Vector2(thisX - x, thisY - y).normalized;

            Player.GetComponent<PlayerController>()?.BumpPlayerIntoDirection(-dist * 10.0f);
            Hit();
        }
    }

    private void Hit()
    {
        //if (this.hitClips != null && this.hitClips.Length != 0)
        //{
        //    this.soundPlayer.clip = this.hitClips.Length == 0 ? null : this.hitClips[this.random.Next(this.hitClips.Length)];
        //    this.soundEstimate = this.soundPlayer.clip.length;

        //    this.soundPlayer.Play();
        //}


        StartCoroutine(TimeHit());
    }

    IEnumerator TimeHit()
    {
        var damage = this.gameObject.GetComponent<EnemyStats>().GetDamage();
        Speed = 0.0f;
        if (Random.Range(0.0f, 1.0f) <= 0.5f)
        {
            animator.SetBool("attack_1", true);
            yield return new WaitForSeconds(10.0f / 60.0f);
            playerController.movement_disabled = true;
            yield return new WaitForSeconds(49.0f / 60.0f);
            Player.GetComponent<PlayerStats>().TakeDamage(damage);
            playerController.movement_disabled = false;
            yield return new WaitForSeconds(102.0f / 60.0f);
            animator.SetBool("attack_1", false);
            Speed = 0.7f;
        }
        else
        {
            animator.SetBool("attack_2", true);
            yield return new WaitForSeconds(10.0f / 60.0f);
            playerController.movement_disabled = true;
            yield return new WaitForSeconds(126.0f / 60.0f);
            Player.GetComponent<PlayerStats>().TakeDamage(damage);
            playerController.movement_disabled = false;
            yield return new WaitForSeconds(66.0f / 60.0f);
            animator.SetBool("attack_2", false);
            Speed = 0.7f;
        }
    }
}
