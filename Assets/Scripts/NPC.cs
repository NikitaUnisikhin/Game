using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject panelDialog;
    public Text dialoge;
    
    private int iter = 0;
    private bool dialogeStart;
    public string[] message = new string[5];

    void Start()
    {
        panelDialog.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            panelDialog.SetActive(true);
            dialoge.text = message[iter];
            dialogeStart = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            panelDialog.SetActive(false);
            dialogeStart = false;
            iter = 0;
        }
    }
    void Update()
    {
        if (iter < message.Length - 1 && dialogeStart && Input.GetKeyDown(KeyCode.R))
        {
            dialoge.text = message[++iter];
        }
    }
}