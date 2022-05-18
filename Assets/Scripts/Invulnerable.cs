using UnityEngine;

public class Invulnerable : Bonus
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();

        if (character)
        {
            character.setInvulnerability(time);
            Destroy(gameObject);
        }
    }
}
