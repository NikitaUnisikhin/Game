using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheEndOFGame : MonoBehaviour
{
    public GameObject panelDialog;
    public Text dialoge;
    public string textOfEnd;

    void Start()
    {
        panelDialog.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            panelDialog.SetActive(true);
            dialoge.text = textOfEnd;
        }
    }
}
