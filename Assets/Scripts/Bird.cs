using UnityEngine;
using System.Linq;

public class Bird : Monster
{
    [SerializeField]
    private float speed = 2.0F;

    public Rigidbody2D rb;
    public LayerMask groundLayers;
    public Transform groundCheck;
    bool isFacingRighy = true;
    RaycastHit2D hit;

    private void Update()
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);
    }

    private void FixedUpdate()
    {
        if (hit.collider != false)
        {
            Debug.Log("Земля!");
        }
        else
        {
            Debug.Log("Нет земли");
        }
    }

}
