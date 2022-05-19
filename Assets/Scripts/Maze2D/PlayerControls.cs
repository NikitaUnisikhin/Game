using UnityEngine;

public class PlayerControls : Unit
{
    public float Speed = 2;
    private Animator animator;
    private SpriteRenderer sprite;

    private Rigidbody2D componentRigidbody;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        componentRigidbody = GetComponent<Rigidbody2D>();
    }

    private CharStateMaze State
    {
        get { return (CharStateMaze)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Update()
    {
        componentRigidbody.velocity = Vector2.zero;

        State = CharStateMaze.Idle;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            sprite.flipX = true;
            State = CharStateMaze.Run;
            componentRigidbody.velocity += Vector2.left * Speed;
            
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            sprite.flipX = false;
            State = CharStateMaze.Run;
            componentRigidbody.velocity += Vector2.right * Speed;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            State = CharStateMaze.Run;
            componentRigidbody.velocity += Vector2.up * Speed;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            State = CharStateMaze.Run;
            componentRigidbody.velocity += Vector2.down * Speed;
        }
    }
}

public enum CharStateMaze
{
    Idle,
    Run
}