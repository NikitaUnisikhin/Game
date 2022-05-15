using UnityEngine;

public class Acceleration : MonoBehaviour
{
    [SerializeField]
    public int Time = 5;

    private AudioSource accelerationClip;

    protected void Awake()
    {
        accelerationClip = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();

        if (character)
        {
            accelerationClip.Play();
            character.setAcceleration(Time);
            Destroy(gameObject);
        }
    }
}
