using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Scoreboard : MonoBehaviour 
{
    public int Score;   

    private Text scoreText;

    // scorable objects in scene
    private Scorable[] scorables;

	// Use this for initialization
	void Start () 
	{
        scoreText = GetComponent<Text>();

        scorables = FindObjectsOfType<Scorable>();
	}
	
    private void updateGlobalScore()
    {
        int sum = 0;
        for (int i = 0; i < scorables.Length; i++)
        {
            sum += scorables[i].Score;
        }

        //if (sum > Score)
            Score = sum;
    }

    public void updateScoreText()
    {
        scoreText.text = "Score: <b>" + Score + "</b>";
    }

	// Update is called once per frame
	void Update () 
	{
        updateGlobalScore();
        updateScoreText();
	}
}
