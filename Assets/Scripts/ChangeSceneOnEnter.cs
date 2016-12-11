using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeSceneOnEnter : MonoBehaviour
{

    public string Scene;
    public float Delay;

    private Coroutine coroutine;

    public void OnTriggerEnter(Collider other)
    {
        coroutine = StartCoroutine(ChangeSceneDelay());
    }

    IEnumerator ChangeSceneDelay()
    {
        yield return new WaitForSeconds(Delay);
        SceneManager.LoadScene(Scene);
    }
}
