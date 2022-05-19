using UnityEngine;

public class Heart : Bonus
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();
        
        if (character)
        {
            character.Lives++;
            Destroy(gameObject);
        }
    }
}
