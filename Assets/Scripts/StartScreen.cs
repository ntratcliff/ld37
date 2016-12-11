using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour 
{
    public string NextScene;
    public Animator Animator;
    public Text Text;
    public float FadeSpeed;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Animator.Stop();
            StartCoroutine(fadeOutText());
        }
	}

    private void setTextAlpha(float a)
    {
        Color c = Text.color;
        c.a = a;
        Text.color = c;
    }

    IEnumerator fadeOutText()
    {
        float t = 0;
        float startAlpha = Text.color.a;
        do
        {
            yield return new WaitForFixedUpdate();

            t += FadeSpeed * Time.deltaTime;

            float alpha = Mathf.Lerp(startAlpha, 0, t);
            setTextAlpha(alpha);
        }
        while (t < 1f);

        setTextAlpha(0);

        SceneManager.LoadSceneAsync(NextScene);
    }
}
