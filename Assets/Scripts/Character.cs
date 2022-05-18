using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class Character : Unit
{
    [SerializeField]
    private int lives = 9;

    [SerializeField]
    private float speed = 3.0F;
    
    [SerializeField]
    private float jumpForce = 10.0F;

    private LivesBar livesBar;
    public Transform groudCheck;
    public LayerMask whatIsGround;
    private Spear spear;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    private bool isGrounded = false;
    public float checkRadius = 0.5f;
    private float force = 5f;
    private float timer = 1f;
    private bool isInvulnerable = false;
    private int extraJumps;
    public int extraJumpsValue = 1;

    private AudioSource JumpClip;
    private AudioSource ShootClip;
    private AudioSource DamageClip;

    public int Lives
    {
        get { return lives; }
        set
        {
            if (value < 9) lives = value;
            livesBar.Refresh();
        }
    }

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        var clips = GetComponents<AudioSource>();
        clips[0].Play();

        JumpClip = clips[1];
        ShootClip = clips[2];
        DamageClip = clips[3];

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
        {
            Shoot();
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
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

        if (isGrounded)
        {
            State = CharState.Run;
        }
    }

    private void Jump()
    {
        JumpClip.Play();
        rb.velocity = Vector2.up * jumpForce;
    }

    private void Shoot()
    {
        ShootClip.Play();

        Vector3 position = transform.position; position.y += 0.4F;
        Spear newSpear = Instantiate(spear, position, spear.transform.rotation);
        newSpear.Parent = gameObject;
        newSpear.rigidbody.AddForce(new Vector2((sprite.flipX ? -1 : 1), 1) * force, ForceMode2D.Impulse);
        timer = 1f;
    }

    public override void ReceiveDamage()
    {
        DamageClip.Play();

        if (!isInvulnerable)
        {
            Lives--;

            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);

            if (lives == 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders
            .Where(collider => collider.tag != "Invisible")
            .ToArray()
            .Length > 1;

        if (!isGrounded)
        {
            State = CharState.Jump;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet projectile = collider.gameObject.GetComponent<Bullet>();

        if (projectile && projectile.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }

    public void setInvulnerability(int timeInSec)
    {
        StartCoroutine(timeMethodForInvulnerability(timeInSec));
    }

    IEnumerator timeMethodForInvulnerability(int timeInSec)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(timeInSec);
        isInvulnerable = false;
    }

    public void setAcceleration(int timeInSec)
    {
        StartCoroutine(timeMethodForAcceleration(timeInSec));
    }

    IEnumerator timeMethodForAcceleration(int timeInSec)
    {
        speed *= 1.5f;
        yield return new WaitForSeconds(timeInSec);
        speed /= 1.5f;
    }

    public void setSuperJump(int timeInSec)
    {
        StartCoroutine(timeMethodForSuperJump(timeInSec));
    }

    IEnumerator timeMethodForSuperJump(int timeInSec)
    {
        jumpForce *= 1.5f;
        yield return new WaitForSeconds(timeInSec);
        jumpForce /= 1.5f;
    }
}


public enum CharState
{
    Idle,
    Run,
    Jump
}
