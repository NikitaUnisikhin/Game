using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CatBoss : Boss
{
    [SerializeField]
    private float speed = 2.0F;
    [SerializeField]
    private float rate = 2F;

    private Bullet bullets;
    private Vector3 direction;

    private bool isFacingLeft = true;
    public Transform groundCheck;
    public LayerMask groundLayers;
    public Rigidbody2D rb;

    private AudioSource ShootClip;

    private SpriteRenderer sprite;

    protected void Awake()
    {
        ShootClip = GetComponent<AudioSource>();
        bullets = Resources.Load<Bullet>("Bullets");
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
        direction = -transform.right;
    }
    protected void Update()
    {
        Move();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.3F) ReceiveDamage();
            else unit.ReceiveDamage();
        }

        Spear spear = collider.gameObject.GetComponent<Spear>();
        if (spear && spear.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }

    private void Shoot()
    {
        ShootClip.Play();
        Vector3 position = transform.position; position.y += 0.4F;
        Bullet newBullets = Instantiate(bullets, position, bullets.transform.rotation);

        newBullets.Parent = gameObject;
        newBullets.Sprite.flipX = !isFacingLeft;
        newBullets.Direction = -newBullets.transform.right * speed / 2;
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5F + transform.right * direction.x * 0.5F, 0.1F);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 1f, groundLayers);

        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Character>() && !x.GetComponent<Spear>() && !x.GetComponent<Bullet>()
                                                    && !x.GetComponent<Bullet>()) || groundInfo.collider == false)
        {
            isFacingLeft = !isFacingLeft;
            speed = -speed;
            direction *= -1.0F;
            transform.localScale = new Vector2(-transform.localScale.x, 1f);
        }
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    public override void ReceiveDamage()
    {
        Lives--;

        rb.velocity = Vector3.zero;
        rb.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);

        if (Lives <= 0) Die();
    }
}