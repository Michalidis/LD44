using Assets.Scripts.Monsters;
using UnityEngine;

public class WallRandomHitter : MonsterMovement
{
    private Vector2 velocity = new Vector2(0.2f, 0.2f);
    private float speed = 0.5f;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        this.body.velocity = this.velocity * this.speed;

        SetAnimator(this.body.velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.speed = (this.random.Next(20) + 50) / 100.0f;

        float x = collision.gameObject.transform.position.x;
        float y = collision.gameObject.transform.position.y;

        float thisX = this.gameObject.transform.position.x;
        float thisY = this.gameObject.transform.position.y;

        Vector2 dist = new Vector2(thisX - x, thisY - y).normalized;
        var randVec = new Vector2(dist.x * 0.9f * this.random.Next(100) / 100.0f, dist.y * 0.9f * this.random.Next(100) / 100.0f);

        this.velocity = (dist + randVec) * this.speed;
    }
}
