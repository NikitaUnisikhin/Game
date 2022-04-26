using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBForDoctor : MonoBehaviour
{
    private Transform[] hearts = new Transform[3];

    private Doctor doctor;


    private void Awake()
    {
        doctor = FindObjectOfType<Doctor>();

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i);
        }
    }

    public void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < doctor.Lives) hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false);
        }
    }
}
