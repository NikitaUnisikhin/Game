using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bird : Monster
{
    [SerializeField]
    private float speed = 2.0f;

    private Vector3 direction;

    protected override void Start()
    {
        direction = -transform.right;
    }

    protected override void Update()
    {
        Move();   
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.3f) ReceiveDamage();
            else unit.ReceiveDamage();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}
