using UnityEngine;

public class Spear : GameProjectile
{
    new public Rigidbody2D rigidbody;
    private bool isShot;
    public float Force;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2F);
        isShot = true;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (isShot)
        {
            var direction = rigidbody.velocity;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
