using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();   
    }

    public void ClickButton(int NumberButton)
    {
        audio.Play();
        SceneManager.LoadScene(NumberButton);
    }
}
