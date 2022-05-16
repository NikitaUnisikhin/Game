using UnityEngine;

public class LivesBarBoss : MonoBehaviour
{
    [SerializeField]
    private int countHearts = 3;

    private Transform[] hearts;
    private Boss boss;

    private void Awake()
    {
        boss = FindObjectOfType<Boss>();
        hearts = new Transform[countHearts];

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i);
        }
    }

    public void Update()
    {
        Debug.Log(boss.Lives);
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < boss.Lives)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }
}
