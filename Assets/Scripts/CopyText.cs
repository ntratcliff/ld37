using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CopyText : MonoBehaviour 
{
    public Text Copy;
	// Update is called once per frame
	void Update () 
	{
        GetComponent<Text>().text = Copy.text;
	}
}
