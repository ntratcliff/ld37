using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour 
{
    private RoundTime timer;
    private Scoreboard scoreboard;

    private IRoundStatusTarget roundStatTargets;

    public GameObject[] EnableOnStart;
    public GameObject[] EnableOnEnd;

	// Use this for initialization
	void Start () 
	{
        timer = FindObjectOfType<RoundTime>();
        scoreboard = FindObjectOfType<Scoreboard>();

        // enable start objects
        for (int i = 0; i < EnableOnStart.Length; i++)
        {
            EnableOnStart[i].SetActive(true);
        }

        // start the round
        executeStatusRecursive(this.gameObject, null, (x, y) => x.RoundStart());
	}

    public void GameOver()
    {
        // enable end objects
        for (int i = 0; i < EnableOnEnd.Length; i++)
        {
            EnableOnEnd[i].SetActive(true);
        }

        // call delegates
        executeStatusRecursive(this.gameObject, null, (x, y) => x.RoundEnd());
    }

    private void executeStatusRecursive(GameObject root, BaseEventData data, ExecuteEvents.EventFunction<IRoundStatusTarget> callbackFunction)
    {
        foreach (Transform child in root.transform)
        {
            executeStatusRecursive(child.gameObject, data, callbackFunction);
        }
        ExecuteEvents.Execute<IRoundStatusTarget>(root, data, callbackFunction);
    }

}
