using UnityEngine;
public class LBForMammoth : MonoBehaviour
{
    private Transform[] hearts = new Transform[3];

    private Mammoth mammoth;


    private void Awake()
    {
        mammoth = FindObjectOfType<Mammoth>();

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i);
        }
    }

    public void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < mammoth.Lives) hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false);
        }
    }
}
