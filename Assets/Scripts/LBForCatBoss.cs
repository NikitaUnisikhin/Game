using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBForCatBoss : MonoBehaviour
{
    private Transform[] hearts = new Transform[3];
    private CatBoss catBoss;

    private void Awake()
    {
        catBoss = FindObjectOfType<CatBoss>();

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i);
        }
    }

    public void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < catBoss.Lives) hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false);
        }
    }
}
