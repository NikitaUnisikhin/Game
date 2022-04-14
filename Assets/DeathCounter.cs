using UnityEngine;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour
{
    private static int deathCount = 0;
    [SerializeField] private Text textUIDeathCount;
    public static DeathCounter deathCounter;

    private void Start()
    {
        deathCounter = this;
    }
    public static void drawText()
    {
        deathCounter.textUIDeathCount.text = "Смертей:  " + deathCount;
    }

    public static void resetDeathCount()
    {
        deathCount = 0;
        drawText();
    }

    public static void upDeath()
    {
        deathCount++;
        drawText();
    }
}