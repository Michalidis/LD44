using Assets.Scripts.Character;
using Assets.Scripts.Monsters;
using UnityEngine;

public class FatMonster : MonsterMovement
{
    public AudioClip[] ZombieMoanClips;
    private AudioClip[] hitClips;
    private AudioSource soundPlayer;

    private float soundEstimate;

    // Start is called before the first frame update
    private void Start()
    {
        Speed = 0.3f;
        this.Init();
        this.soundPlayer = this.GetComponent<AudioSource>();
        this.hitClips = GameObject.Find("HitManager").GetComponent<HitManager>().HitClips;
    }

    // Update is called once per frame
    private void Update()
    {
        this.PlayMoan();

        Vector3 plPosition = this.Player.gameObject.transform.position;
        float x = plPosition.x;
        float y = plPosition.y;

        float thisX = this.gameObject.transform.position.x;
        float thisY = this.gameObject.transform.position.y;

        Vector2 dist = new Vector2(x - thisX, y - thisY).normalized;

        this.body.velocity = dist * this.Speed;

        this.SetAnimator(this.body.velocity);
    }

    private void PlayMoan()
    {
        if (soundEstimate < 0)
        {
            this.soundPlayer.clip = this.ZombieMoanClips.Length == 0 ? null : this.ZombieMoanClips[this.random.Next(this.ZombieMoanClips.Length)];
            this.soundEstimate = this.soundPlayer.clip.length + 5 + this.random.Next(5);

            this.soundPlayer.Play();
        }
        soundEstimate -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == this.Player)
        {
            float x = collision.gameObject.transform.position.x;
            float y = collision.gameObject.transform.position.y;

            float thisX = this.gameObject.transform.position.x;
            float thisY = this.gameObject.transform.position.y;

            Vector2 dist = new Vector2(thisX - x, thisY - y).normalized;

            this.Player.GetComponent<PlayerController>()?.BumpPlayerIntoDirection(-dist * 10.0f);
            Hit();
        }
    }

    private void Hit()
    {
        if (this.hitClips != null && this.hitClips.Length != 0)
        {
            this.soundPlayer.clip = this.hitClips.Length == 0 ? null : this.hitClips[this.random.Next(this.hitClips.Length)];
            this.soundEstimate = this.soundPlayer.clip.length;

            this.soundPlayer.Play();
        }

        var damage = this.gameObject.GetComponent<EnemyStats>().GetDamage();
        this.Player.GetComponent<PlayerStats>().TakeDamage(damage);
    }
}
