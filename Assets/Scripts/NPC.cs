using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject panelDialog;
    int iter = 0;
    public Text dialoge;
    bool dialogeStart;
    public string[] message = {"Ох ты господи! Кто ты такой?",
                               "Ты не похож на местного. Шерсть у тебя ухоженная, брюхо большое. Питаешься ты, видимо, хорошо.",
                               "Будь аккуратней, место тут неспокойное. Берегись котов с копьями, они уже давно не ели и будут не прочь поживиться тобой.",
                               "А птицы тут какие! Даже на живых котов кидаются! Страх совсем потеряли!",
                               "Удачи тебе путник в твоих опасных приключениях!"};
    
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
            iter = 2;
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
