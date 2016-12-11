using UnityEngine;
using System.Collections;
using System;

public class Screenshot : MonoBehaviour
{
    public KeyCode Key;
    public GameObject[] DisableForShot;
    public GameObject[] EnableForShot;
    public string Prefix = "Screenshot_";


    private void setShotObjectsState(GameObject[] arr ,bool state)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].SetActive(state);
        }
    }

    private IEnumerator captureScreen()
    {
        setShotObjectsState(DisableForShot, false);
        setShotObjectsState(EnableForShot, true);

        yield return new WaitForEndOfFrame();

        // take screenshot
        string filename = Prefix + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png";
        Application.CaptureScreenshot(filename);
        Debug.Log(filename);

        setShotObjectsState(DisableForShot, true);
        setShotObjectsState(EnableForShot, false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(Key))
            StartCoroutine(captureScreen());
    }
}
