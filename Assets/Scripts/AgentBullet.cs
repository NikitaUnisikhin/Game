using UnityEngine;

public class AgentBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0F;

    public Rigidbody2D rb;

    public GameObject Parent { get; set; }
    public Vector3 Direction { get; set; }
    public SpriteRenderer Sprite { get; set; }

    private void Awake()
    {
        Sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, 2F);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if ((unit && unit.gameObject != Parent) || collider.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
