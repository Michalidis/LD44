using Assets.Scripts.Character;
using Assets.Scripts.Monsters;
using UnityEngine;

public class Horde : MosterMovement
{
    public float Speed = 0.4f;

    public float MaxLeaderDistance = 0.4f;

    private GameObject Leader;
    private AudioSource soundPlayer;
    private AudioClip[] hitClips;

    private float SoundEstimate;
    private bool hitting;
    private readonly System.Random luck = new System.Random();

    // Start is called before the first frame update
    private void Start()
    {
        this.Init();
        this.soundPlayer = this.GetComponent<AudioSource>();
        this.hitClips = GameObject.Find("HitManager").GetComponent<HitManager>().HitClips;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 thisPos = this.gameObject.transform.position;

        Vector3 plPosition = this.Player.gameObject.transform.position;
        float plX = plPosition.x;
        float plY = plPosition.y;
        var plDist = new Vector2(plX - thisPos.x, plY - thisPos.y);

        if (this.Leader == null)
        {
            this.col.velocity = plDist.normalized * this.Speed;
        }
        else
        {
            Vector3 leaderPosition = this.Leader.transform.position;
            float x = leaderPosition.x;
            float y = leaderPosition.y;
            var leaderDist = new Vector2(x - thisPos.x, y - thisPos.y);

            if (leaderDist.magnitude < this.MaxLeaderDistance || plDist.magnitude < leaderDist.magnitude)
            {
                this.col.velocity = plDist.normalized * this.Speed;
            }
            else if (leaderDist.magnitude > this.MaxLeaderDistance)
            {
                this.col.velocity = leaderDist.normalized * this.Speed;
            }
        }

        this.Hit();
        this.SetAnimator(this.col.velocity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Horde>() != null)
        {
            this.Leader = other.gameObject;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == this.Player)
        {
            this.hitting = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == this.Player)
        {
            this.hitting = false;
        }
    }

    private void Hit()
    {
        if (!this.hitting)
        {
            return;
        }
        
        if (this.SoundEstimate < 0)
        {
            if (this.hitClips != null && this.hitClips.Length != 0)
            {
                this.soundPlayer.clip = this.hitClips.Length == 0 ? null : this.hitClips[this.luck.Next(this.hitClips.Length)];
                this.SoundEstimate = this.soundPlayer.clip.length;

                this.soundPlayer.Play();
            }

            var damage = this.gameObject.GetComponent<EnemyStats>().GetDamage();
            this.Player.GetComponent<PlayerStats>().TakeDamage(damage);
        }

        this.SoundEstimate -= Time.deltaTime;
    }
}
