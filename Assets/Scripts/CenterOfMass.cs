using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    public Vector2 com = new Vector2(1,0);
    public Rigidbody2D rb;

    void Start()
    {
        rb.centerOfMass = com;
    }
}
