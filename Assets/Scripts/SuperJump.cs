using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    public int time = 5;

    private AudioSource BuffClip;

    protected void Awake()
    {
        BuffClip = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();

        if (character)
        {
            BuffClip.Play();
            character.setSuperJump(time);
            Destroy(gameObject);
        }
    }
}
