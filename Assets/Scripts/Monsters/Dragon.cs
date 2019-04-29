using Assets.Interfaces;
using Assets.Scripts.Character;
using Assets.Scripts.Monsters;
using Assets.Scripts.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonsterMovement, IWeaponProjectileEmitter
{
    public AudioClip[] growls;
    private float soundEstimate;

    private PlayerController playerController;
    public GameObject Fireball;

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

    IEnumerator FireballSequence()
    {
        Speed = 0.0f;
        animator.SetBool("attack_2", true);
        yield return new WaitForSeconds(66.0f / 60.0f);
        ShootFireballs();
        soundPlayer.Play();
        yield return new WaitForSeconds(136.0f / 60.0f);
        animator.SetBool("attack_2", false);
        Speed = 0.7f;
    }
    void PlayGrowl()
    {
        if (soundEstimate < 0)
        {
            StartCoroutine(FireballSequence());
            soundPlayer.clip = growls[random.Next(growls.Length)];
            soundEstimate = soundPlayer.clip.length + 5 + random.Next(5);
        }
        soundEstimate -= Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        PlayGrowl();

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

    private void ShootFireballs()
    {
        for (var i = 0; i < 18; i++)
        {
            GameObject fb = Instantiate(Fireball, transform.position, transform.rotation);
            fb.GetComponent<ProjectileBehavior>().shotFrom = GetComponent<IWeaponProjectileEmitter>();
            fb.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(10 * i) * 75.0f, Mathf.Cos(10 * i) * 75.0f));
            StartCoroutine(DestroyFireball(fb));
        }
    }

    IEnumerator DestroyFireball(GameObject fb)
    {
        yield return new WaitForSeconds(2.5f);
        if (fb != null)
        {
            Destroy(fb);
        }
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
        StartCoroutine(TimeHit());
    }

    IEnumerator TimeHit()
    {
        var damage = this.gameObject.GetComponent<EnemyStats>().GetDamage();
        Speed = 0.0f;
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

    public int GetDamage()
    {
        return Mathf.RoundToInt(GetComponent<EnemyStats>().GetDamage() * Random.Range(0.6f, 1.4f));
    }
}
