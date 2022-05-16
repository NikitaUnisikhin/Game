using UnityEngine;

public class Bullet : GameProjectile
{
    private void Start()
    {
        Destroy(gameObject, 2F);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Direction, speed * Time.deltaTime);
    }
}
