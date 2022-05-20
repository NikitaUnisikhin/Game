using UnityEngine;

public class SuperJump : Bonus
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();

        if (character)
        {
            character.setSuperJump(time);
            Destroy(gameObject);
        }
    }
}