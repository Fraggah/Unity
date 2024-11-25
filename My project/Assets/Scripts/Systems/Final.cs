using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Final : MonoBehaviour
{
    [SerializeField]
    private float delay = 5f;

    [SerializeField]
    private string sceneName = "Menu"; 

    private void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName);
    }
}
