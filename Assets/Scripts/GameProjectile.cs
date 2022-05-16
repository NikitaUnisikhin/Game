using UnityEngine;

public class GameProjectile : MonoBehaviour
{
    [SerializeField]
    protected float speed = 10.0F;

    public GameObject Parent { get; set; }
    public Vector3 Direction { get; set; }
    public SpriteRenderer Sprite { get; set; }

    private void Awake()
    {
        Sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit.gameObject != Parent || collider.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
