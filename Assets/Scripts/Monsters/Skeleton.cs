using UnityEngine;

public class Skeleton : MosterMovement
{
    private Vector2 velocity = new Vector2(0.2f, 0.2f);
    public float Speed = 0.5f;

    private void Start()
    {
        this.Init();
    }

    private void Update()
    {
        this.body.velocity = this.velocity * this.Speed;

        this.SetAnimator(this.body.velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.Speed = (this.random.Next(20) + 50) / 100.0f;

        var collisionPoint = collision.GetContact(0).point;

        float thisX = this.gameObject.transform.position.x;
        float thisY = this.gameObject.transform.position.y;

        Vector2 dist = new Vector2(thisX - collisionPoint.x, thisY - collisionPoint.y).normalized;
        var randVec = new Vector2(dist.x * 0.9f * this.random.Next(100) / 100.0f, dist.y * 0.9f * this.random.Next(100) / 100.0f);

        this.velocity = (dist + randVec) * this.Speed;
    }
}
