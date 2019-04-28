using UnityEngine;

public class DistanceTaker : MonoBehaviour
{
    public GameObject Player { get; set; }

    public float Speed { get; set; } = 0.3f;

    public float MinDistance { get; set; } = 0.8f;

    public float MaxDistance { get; set; } = 1.5f;

    private Rigidbody2D col;

    // Start is called before the first frame update
    private void Start()
    {
        if (this.Player == null)
        {
            this.Player = GameObject.Find("Character");
        }

        this.col = this.gameObject.GetComponent<Rigidbody2D>();
        this.col.rotation = 0;
        this.col.freezeRotation = true;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 plPosition = this.Player.gameObject.transform.position;
        float x = plPosition.x;
        float y = plPosition.y;

        float thisX = this.gameObject.transform.position.x;
        float thisY = this.gameObject.transform.position.y;

        var dist = new Vector2(x - thisX, y - thisY);

        if (MinDistance < dist.magnitude && dist.magnitude < MaxDistance)
        {
            this.col.velocity = Vector2.zero;
        }
        else if (dist.magnitude < MinDistance)
        {
            this.col.velocity = -dist.normalized * this.Speed;
        }
        else
        {
            this.col.velocity = dist.normalized * this.Speed;
        }
    }
}
