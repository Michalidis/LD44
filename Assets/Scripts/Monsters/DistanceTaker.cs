using UnityEngine;

public class DistanceTaker : MosterMovement
{
    public float Speed = 0.3f;

    public float MinDistance = 0.8f;

    public float MaxDistance = 1.5f;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
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
            this.body.velocity = Vector2.zero;
        }
        else if (dist.magnitude < MinDistance)
        {
            this.body.velocity = -dist.normalized * this.Speed;
        }
        else
        {
            this.body.velocity = dist.normalized * this.Speed;
        }

        this.SetAnimator(this.body.velocity);
    }
}
