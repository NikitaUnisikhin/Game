using UnityEngine;
using System.Collections;

public class Monster : Unit
{
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Spear spear = collider.GetComponent<Spear>();

        if (spear)
        {
            ReceiveDamage();
        }

        Character character = collider.GetComponent<Character>();

        if (character)
        {
            character.ReceiveDamage();
        }
    }
}
