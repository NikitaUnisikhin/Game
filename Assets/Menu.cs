using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();   
    }

    public void ClickButton(string nameOfScene)
    {
        audio.Play();
        SceneManager.LoadScene(nameOfScene);
    }

    public void Exit()
    {
        audio.Play();
        Application.Quit();
    }
}
