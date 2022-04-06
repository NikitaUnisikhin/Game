using UnityEngine;
using System.Collections;

public class ShootableMonster : Monster
{
    [SerializeField]
    private float rate = 2.0F;
    [SerializeField]
    private Color SpearColor = Color.white;

    private Spear spear;

    protected override void Awake()
    {
        spear = Resources.Load<Spear>("Spear");
    }

    protected override void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
    }

    private void Shoot()
    {
        Vector3 position = transform.position;          position.y += 0.5F;
        Spear newSpear = Instantiate(spear, position, spear.transform.rotation) as Spear;

        newSpear.Parent = gameObject;
        newSpear.Direction = -newSpear.transform.right;
        newSpear.Color = SpearColor;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();




        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.3F) ReceiveDamage();
            else unit.ReceiveDamage();
        }
    }
}
