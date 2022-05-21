using UnityEngine;

public class Cannon : Monster
{
    [SerializeField]
    private float rate = 1F;

    private Bullet shell;
    
    public Rigidbody2D rb;
    
    private AudioSource shootClip;

    protected void Awake()
    {
        shootClip = GetComponent<AudioSource>();
        shell = Resources.Load<Bullet>("Shell");
    }

    protected void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
    }

    private void Shoot()
    {
        shootClip.Play();
        Vector3 position = transform.position; position.y += 0.32F; position.x -= 0.3f;
        Bullet newBullet = Instantiate(shell, position, shell.transform.rotation);

        newBullet.Parent = gameObject;
        newBullet.Direction = -newBullet.transform.right;
    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x - 0.5f) < 0.6F
                && Mathf.Abs(unit.transform.position.y - transform.position.y) > 0.3f)
                ReceiveDamage();
            else unit.ReceiveDamage();
        }

        Spear spear = collider.gameObject.GetComponent<Spear>();

        if (spear && spear.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }
}
