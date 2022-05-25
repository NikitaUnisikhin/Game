using System.Linq;
using UnityEngine;

public class Ghost : Unit
{
    [SerializeField]
    private float speed = 2.0F;

    private Vector3 direction;

    private bool isFacingLeft = true;

    public Rigidbody2D rb;

    protected void Start()
    {
        direction = -transform.right;
    }
    protected void Update()
    {
        // Move();
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is PlayerControls)
        {
            unit.ReceiveDamage();
        }
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up + transform.right * direction.x * 0.5F, 0.0001F);

        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<PlayerControls>()))
        {
            isFacingLeft = !isFacingLeft;
            speed = -speed;
            direction *= -1.0F;
            transform.localScale = new Vector2(-transform.localScale.x, 1f);
        }

        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }
}
