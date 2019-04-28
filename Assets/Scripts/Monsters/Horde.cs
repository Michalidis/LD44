using System.Collections;
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

    private bool followPlayer = false;

    // Start is called before the first frame update
    private void Start()
    {
        this.Init();
        this.soundPlayer = this.GetComponent<AudioSource>();
        this.hitClips = GameObject.Find("HitManager").GetComponent<HitManager>().HitClips;

        this.StartCoroutine("FollowPlayer");
    }

    private IEnumerator FollowPlayer()
    {
        yield return new WaitForSeconds(5.0f); // wait half a second
        this.followPlayer = true;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 thisPos = this.gameObject.transform.position;

        Vector3 plPosition = this.Player.gameObject.transform.position;
        float plX = plPosition.x;
        float plY = plPosition.y;
        var plDist = new Vector2(plX - thisPos.x, plY - thisPos.y);

        if (followPlayer || this.Leader == null)
        {
            this.body.velocity = plDist.normalized * this.Speed;
        }
        else
        {
            Vector3 leaderPosition = this.Leader.transform.position;
            float x = leaderPosition.x;
            float y = leaderPosition.y;
            var leaderDist = new Vector2(x - thisPos.x, y - thisPos.y);

            if (leaderDist.magnitude < this.MaxLeaderDistance || plDist.magnitude < leaderDist.magnitude)
            {
                this.body.velocity = plDist.normalized * this.Speed;
            }
            else if (leaderDist.magnitude > this.MaxLeaderDistance)
            {
                this.body.velocity = leaderDist.normalized * this.Speed;
            }
        }

        this.PlayHit();
        this.SetAnimator(this.body.velocity);
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

    private void PlayHit()
    {
        if (!this.hitting)
        {
            return;
        }

        if (this.SoundEstimate < 0)
        {
            if (this.hitClips != null && this.hitClips.Length != 0)
            {
                this.soundPlayer.clip = this.hitClips.Length == 0 ? null : this.hitClips[this.random.Next(this.hitClips.Length)];
                this.SoundEstimate = this.soundPlayer.clip.length;

                this.soundPlayer.Play();
            }
        }
        this.SoundEstimate -= Time.deltaTime;
    }
}
