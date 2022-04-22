using UnityEngine;
using System.Collections;

public class LivesBar : MonoBehaviour
{
    private Transform[] hearts = new Transform[9];

    private Character character;


    private void Awake()
    {
        character = FindObjectOfType<Character>();

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i);
<<<<<<< HEAD
            // Debug.Log(i);
=======
>>>>>>> 9a2ee38fd784160c190e9f7d9905c3d3768975ae
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < character.Lives) hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false);
        }
    }
}