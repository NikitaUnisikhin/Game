using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Monster
{
    [SerializeField]
    private float rate = 1F;

    private Shell shell;
    public Rigidbody2D rb;

    protected void Awake()
    {
        shell = Resources.Load<Shell>("Shell");
    }

    protected void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
    }

    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 0.32F; position.x -= 0.3f;
        Shell newShell = Instantiate(shell, position, shell.transform.rotation);

        newShell.Parent = gameObject;
        newShell.Direction = -newShell.transform.right;
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
