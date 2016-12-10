using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour 
{
    private RoundTime timer;
    private Scoreboard scoreboard;

    private IRoundStatusTarget roundStatTargets;

	// Use this for initialization
	void Start () 
	{
        timer = FindObjectOfType<RoundTime>();
        scoreboard = FindObjectOfType<Scoreboard>();

        // start the round
        executeStatusRecursive(this.gameObject, null, (x, y) => x.RoundStart());
	}

    public void GameOver()
    {
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
