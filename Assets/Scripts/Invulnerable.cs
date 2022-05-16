using UnityEngine;

public class Invulnerable : MonoBehaviour
{
    [SerializeField]
    public int time = 5;

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
            character.setInvulnerability(time);
            Destroy(gameObject);
        }
    }
}
