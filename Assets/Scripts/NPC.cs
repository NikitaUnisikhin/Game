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
    public string[] message = {"�� �� �������! ��� �� �����?",
                               "�� �� ����� �� ��������. ������ � ���� ���������, ����� �������. ��������� ��, ������, ������.",
                               "���� ����������, ����� ��� �����������. �������� ����� � �������, ��� ��� ����� �� ��� � ����� �� ����� ���������� �����.",
                               "� ����� ��� �����! ���� �� ����� ����� ��������! ����� ������ ��������!",
                               "����� ���� ������ � ����� ������� ������������!"};
    
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
