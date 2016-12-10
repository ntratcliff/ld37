using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Text))]
public class RoundTime : MonoBehaviour
{
    public int StartTime = 60;

    private float elapsedTime;

    [System.NonSerialized]
    public float TimeRemaining;

    public bool RoundActive
    {
        get { return TimeRemaining > 0; }
    }

    private Text timeText;
    private GameManager manager;

    // Use this for initialization
    void Start()
    {
        TimeRemaining = StartTime;

        timeText = GetComponent<Text>();
        timeText.text = StartTime.ToString();

        manager = FindObjectOfType<GameManager>();
    }

    private void updateTimeRemaining()
    {
        elapsedTime += Time.deltaTime;
        TimeRemaining = StartTime - elapsedTime;

        if (TimeRemaining < 0) // round over
        {
            TimeRemaining = 0;
            manager.GameOver();
        }
    }

    private void updateText()
    {
        timeText.text = Mathf.RoundToInt(TimeRemaining).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeRemaining > 0)
            updateTimeRemaining();

        updateText();
    }
}
