using UnityEngine;

public class Acceleration : MonoBehaviour
{
    [SerializeField]
    private int time = 5;

    private AudioSource buffClip;

    protected void Awake()
    {
        buffClip = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();

        if (character)
        {
            buffClip.Play();
            character.setAcceleration(time);
            Destroy(gameObject);
        }
    }
}
