using UnityEngine;

public class Monster : MonoBehaviour
{
    private Rigidbody2D col;
    private readonly System.Random luck = new System.Random();
    private Vector2 velocity = new Vector2(0.2f, 0.2f);
    private float speed = 0.5f;

    private void Start()
    {
        this.col = this.gameObject.GetComponent<Rigidbody2D>();
        this.col.rotation = 0;
        this.col.freezeRotation = true;
    }
    
    private void Update()
    {
        this.col.velocity = this.velocity * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.speed = (this.luck.Next(20) + 50) / 100.0f;

        float x = collision.gameObject.transform.position.x;
        float y = collision.gameObject.transform.position.y;

        float thisX = this.gameObject.transform.position.x;
        float thisY = this.gameObject.transform.position.y;

        Vector2 dist = new Vector2(thisX - x, thisY - y).normalized;
        var randVec = new Vector2(dist.x * 0.9f * luck.Next(100) / 100.0f, dist.y * 0.9f * luck.Next(100) / 100.0f);

        this.velocity = (dist + randVec) * this.speed;
    }
}
