using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mammoth : Boss
{
    [SerializeField]
    private float speed = 3.0F;

    private const float constantSpeed = 3.0F;
    private const float time = 2.0F;

    private Vector3 direction;
    private bool isFacingLeft = true;
    public Transform groundCheck;
    public LayerMask groundLayers;
    public Rigidbody2D rb;
    private Animator animator;

    private MammothState State
    {
        get { return (MammothState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected void Start()
    {
        direction = -transform.right;
    }
    protected void Update()
    {
        StartCoroutine(YourCoroutine());
        Move();
    }
    IEnumerator YourCoroutine()
    {
        yield return new WaitForSeconds(time);
        if (Mathf.Abs(speed) == constantSpeed)
        {
            State = MammothState.Boost;
            speed *= 2;
        }
        else if (isFacingLeft)
        {
            State = MammothState.Run;
            speed = constantSpeed;
        }
        else
        {
            State = MammothState.Run;
            speed = -constantSpeed;
        }
        StopAllCoroutines();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.9F
                && Mathf.Abs(unit.transform.position.y - transform.position.y) > 0.6f)
                ReceiveDamage();
            else
            {
                unit.ReceiveDamage();
                if (Mathf.Abs(speed) == constantSpeed * 2)
                    unit.ReceiveDamage();
            }
        }

        Spear spear = collider.gameObject.GetComponent<Spear>();
        if (spear)
        {
            ReceiveDamage();
        }
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.3F + transform.right * direction.x * 0.5F, 0.1F);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.5f, groundLayers);

        if (colliders.Length > 1 && colliders.All(x => !x.GetComponent<Character>() && !x.GetComponent<Spear>()) || groundInfo.collider == false)
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

public enum MammothState
{
    Run,
    Boost
}