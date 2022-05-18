using UnityEngine;

public class Acceleration : Bonus
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();

        if (character)
        {
            character.setAcceleration(time);
            Destroy(gameObject);
        }
    }
}
