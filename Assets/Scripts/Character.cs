using UnityEngine;
using System.Collections;

public class Character : Unit
{
    [SerializeField]
    private int lives = 9;

    public int Lives
    {
        get { return lives; }
        set
        {
            if (value < 9) lives = value;
            livesBar.Refresh();
        }
    }
    private LivesBar livesBar;

    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 10.0F;

    private float force = 5f;

    private bool isGrounded = false;
    public Transform groudCheck;
    public float checkRadius = 0.5f;
    public LayerMask whatIsGround;
    private float timer = 1f;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private int extraJumps;
    public int extraJumpsValue = 1;

    private Spear spear;

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    private void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        spear = Resources.Load<Spear>("Spear");
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded)
        {
            State = CharState.Idle;
            extraJumps = extraJumpsValue;
        }

        if (Input.GetButtonDown("Fire1") && timer <= 0)
            Shoot();
        else 
            timer -= Time.deltaTime;

        if (Input.GetButton("Horizontal")) Run();
        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            Jump();
            extraJumps--;
        }
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0F;

        if (isGrounded) State = CharState.Run;
    }

    private void Jump()
    {
        rigidbody.velocity = Vector2.up * jumpForce;
    }

    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 0.4F;
        Spear newSpear = Instantiate(spear, position, spear.transform.rotation);
        newSpear.Parent = gameObject;
        newSpear.rigidbody.AddForce(new Vector2((sprite.flipX ? -1 : 1), 1) * force, ForceMode2D.Impulse);
        timer = 1f;
    }

    public override void ReceiveDamage()
    {
        Lives--;

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);

        if (lives == 0) Die();
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Length > 1;

        if (!isGrounded)
        {
            State = CharState.Jump;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        Spear spear = collider.gameObject.GetComponent<Spear>();
        if (spear && spear.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }
}


public enum CharState
{
    Idle,
    Run,
    Jump
}
