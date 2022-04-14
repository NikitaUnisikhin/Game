using UnityEngine;
using System.Collections;

public class Character : Unit
{
    [SerializeField]
    private int lives = 5;

    public int Lives
    {
        get { return lives; }
        set
        {
           if (value < 5) lives = value;
            livesBar.Refresh();
        }
    }
    private LivesBar livesBar;

    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 10.0F;

    private bool isGrounded = false;
    public Transform groudCheck;
    public float checkRadius = 0.5f;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue = 1;

    private Spear spear;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

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
        isGrounded = Physics2D.OverlapCircle(groudCheck.position, checkRadius, whatIsGround); 
    }

    private void Update()
    {
        if (isGrounded) 
        {
            State = CharState.Idle;
            extraJumps = extraJumpsValue;
        } 

        if (Input.GetButtonDown("Fire1")) Shoot();
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
        newSpear.Sprite.flipX = !sprite.flipX;
        newSpear.Parent = gameObject;
        newSpear.Direction = newSpear.transform.right * (sprite.flipX ? -1.0F : 1.0F);
    }

    public override void ReceiveDamage()
    {
        Lives--;

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);

        Debug.Log(lives);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Length > 1;

        if (!isGrounded) State = CharState.Jump;
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