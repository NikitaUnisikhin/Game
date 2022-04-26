using UnityEngine;
using System.Collections;

public class Spear : MonoBehaviour
{
    private GameObject parent;
    public GameObject Parent { set { parent = value; }  get { return parent; } }

    private float speed = 10.0F;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    private SpriteRenderer sprite;
    public SpriteRenderer Sprite { set { sprite = value; } get { return sprite; } }

    new public Rigidbody2D rigidbody;
    private bool isShot;
    public float Force;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2F);
        isShot = true;
    }

    private void Update()
    {
        Rotate();
    }



    private void Rotate()
    {
        if (isShot)
        {
            var direction = rigidbody.velocity;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if ((unit && unit.gameObject != parent) || collider.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
