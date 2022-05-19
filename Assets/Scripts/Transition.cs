using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField]
    public string targetScene;

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && (unit is Character || unit is PlayerControls))
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
