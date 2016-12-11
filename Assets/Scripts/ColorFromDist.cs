using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorFromDist : MonoBehaviour 
{
    public Transform A, B;

    public Color MinColor, MaxColor;
    public float MinDist, MaxDist;

    Image uiImage;

	// Use this for initialization
	void Start () 
	{
        uiImage = GetComponent<Image>();	
	}
	
	// Update is called once per frame
	void Update () 
	{
        // get distance between A and B
        float dist = Vector3.Distance(A.position, B.position);

        // get scalar from dist
        float t = (dist - MinDist) / (MaxDist - MinDist);

        // lerp color by scalar
        uiImage.color = Color.Lerp(MinColor, MaxColor, t);
	}
}
