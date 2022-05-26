using UnityEngine;

public class ShootingInMaze : MonoBehaviour
{
    private Bullet spear;
    private SpriteRenderer sprite;
    private Rigidbody2D componentRigidbody;
    private AudioSource ShootClip;
    private float timer = 1f;

    protected void Awake()
    {
        ShootClip = GetComponent<AudioSource>();
        spear = Resources.Load<Bullet>("SpearInMaze");
    }

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        componentRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && timer <= 0)
        {
            Shoot();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        ShootClip.Play();
        Vector3 position = transform.position; position.y += 0.1F;
        Bullet newBullets = Instantiate(spear, position, spear.transform.rotation);

        newBullets.Parent = gameObject;
        newBullets.Sprite.flipX = !sprite.flipX;
        newBullets.Direction = -newBullets.transform.right * (sprite.flipX ? 1 : -1);
        timer = 1f;
    }
}
