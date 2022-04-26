using UnityEngine;
using System.Linq;

public class RobberWithCannon : Monster
{
    [SerializeField]
    private float rate = 2.0F;

    private Spear spear;
    public Rigidbody2D rb;
    private float force = 5f;

    protected void Awake()
    {
        spear = Resources.Load<Spear>("Spear");
    }

    protected void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
    }

    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 0.5F;
        Spear newSpear = Instantiate(spear, position, spear.transform.rotation);
        newSpear.Parent = gameObject;
        newSpear.rigidbody.AddForce(new Vector2(-1, 1) * force, ForceMode2D.Impulse);
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
}
