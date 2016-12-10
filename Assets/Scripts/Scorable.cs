using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Scorable : MonoBehaviour 
{
    public float Value = 1f;

    private float sum;
    private float maxDeltaScore;

    public int Score
    {
        get { return Mathf.RoundToInt(sum); }
    }

    // scoring variables
    private Vector3 rootPos;
    private Vector3 rootAngles;
    private float mass;

	// Use this for initialization
	void Start () 
	{
        rootPos = transform.position;
        rootAngles = transform.eulerAngles;
        mass = GetComponent<Rigidbody>().mass;
	}
	
    private void scoreObject()
    {
        // get delta orientation
        Vector3 deltaPos = transform.position - rootPos;
        Vector3 deltaAngle = transform.eulerAngles - rootAngles;

        // convert angles from degrees to radians before getting scalar
        deltaAngle *= Mathf.Deg2Rad;

        // calculate delta orienation score
        float deltaScore = (deltaPos.magnitude) * mass * Value;
        if(deltaScore > maxDeltaScore)
            maxDeltaScore = deltaScore;

        // set sum as max delta orientation score (TODO: velocity and impact scoring?)
        sum = maxDeltaScore;
    }

	// Update is called once per frame
	void Update () 
	{
        scoreObject();
	}
}
