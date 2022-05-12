using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

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

    private bool isInvulnerable = false;



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
        GetComponent<AudioSource>().Play();

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
        if (!isInvulnerable)
        {
            Lives--;

            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);

            if (lives == 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Where(collider => collider.tag != "Invisible").ToArray().Length > 1;

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

        Shell shell = collider.gameObject.GetComponent<Shell>();
        if (shell && shell.Parent != gameObject)
        {
            ReceiveDamage();
        }

        AgentBullet agentBullet = collider.gameObject.GetComponent<AgentBullet>();
        if (agentBullet && agentBullet.Parent != gameObject)
        {
            ReceiveDamage();
        }

        Bullets bullets = collider.gameObject.GetComponent<Bullets>();
        if (bullets && bullets.Parent != gameObject)
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
