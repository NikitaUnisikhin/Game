using UnityEngine;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour
{
    [SerializeField]
    private Text textUIDeathCount;

    private static int deathCount = 0;
    public static DeathCounter deathCounter;

    private void Start()
    {
        deathCounter = this;
    }
    public static void drawText()
    {
        deathCounter.textUIDeathCount.text = "???????:  " + deathCount;
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