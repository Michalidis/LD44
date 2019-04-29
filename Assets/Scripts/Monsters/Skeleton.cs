using Assets.Scripts.Monsters;
using UnityEngine;

public class Skeleton : MonsterMovement
{
    private Vector2 velocity = new Vector2(0.2f, 0.2f);
    public AudioClip[] SkeletonMovementClips;
    private AudioSource soundPlayer;
    private float soundEstimate;

    private void Start()
    {
        Speed = 0.5f;
        this.Init();
        this.Spawn();
        this.soundPlayer = this.GetComponent<AudioSource>();
        if (SkeletonMovementClips.Length != 0)
        {
            this.soundPlayer.clip = this.SkeletonMovementClips[this.random.Next(this.SkeletonMovementClips.Length)];
        }
    }

    private void Update()
    {
        PlayMovementSound();
        this.body.velocity = this.velocity * this.Speed;
        this.SetAnimator(this.body.velocity);
    }

    private void PlayMovementSound()
    {
        if (soundEstimate < 0)
        {
            this.soundEstimate = this.soundPlayer.clip.length + 5 + this.random.Next(5);
            if (SkeletonMovementClips.Length != 0)
            {
                this.soundPlayer.clip = this.SkeletonMovementClips[this.random.Next(this.SkeletonMovementClips.Length)];

                this.soundPlayer.Play();
            }
        }
        else
        {
            soundEstimate -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.Speed = (this.random.Next(20) + 50) / 100.0f;

        var collisionPoint = collision.GetContact(0).point;

        Vector3 thisPos = this.gameObject.transform.position;

        Vector2 dist = new Vector2(thisPos.x - collisionPoint.x, thisPos.y - collisionPoint.y).normalized;
        var randVec = new Vector2(dist.x * 0.9f * this.random.Next(100) / 100.0f, dist.y * 0.9f * this.random.Next(100) / 100.0f);

        Vector3 plPosition = this.Player.gameObject.transform.position;
        float plX = plPosition.x;
        float plY = plPosition.y;
        var plDist = new Vector2(plX - thisPos.x, plY - thisPos.y);

        if (plDist.magnitude > 2.0f)
        {
            this.velocity = plDist.normalized * this.Speed;
        }
        else
        {
            this.velocity = (dist + randVec) * this.Speed;
        }
    }
}
