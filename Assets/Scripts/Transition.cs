using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField]
    public string targetScene;

    [SerializeField]
    public int timeMax = 0;

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && (unit is Character || unit is PlayerControls))
        {
            StartCoroutine(ExampleCoroutine());
        }
    }
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(timeMax);
        SceneManager.LoadScene(targetScene);
    }
}
